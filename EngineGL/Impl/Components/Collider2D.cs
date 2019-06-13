using System;
using System.Collections.Concurrent;
using EngineGL.Core;
using EngineGL.Structs.Math;
using EngineGL.Utils;

namespace EngineGL.Impl.Components
{
    public class Collider2D : Component, ICollider, ICollision
    {
        private ConcurrentDictionary<Guid, CollisionData2D> _collisionDatas =
            new ConcurrentDictionary<Guid, CollisionData2D>();

        public Vec3 Bounds { get; set; }
        public Vec3 Offset { get; set; }

        public virtual void OnCollisionEnter(IGameObject gameObject)
        {
        }

        public virtual void OnCollisionStay(IGameObject gameObject)
        {
        }

        public virtual void OnCollisionLeave(IGameObject gameObject)
        {
        }

        public override void OnUpdate(double deltaTime)
        {
            IScene scene = GameObject.Scene;
            IObject[] objects = scene.GetObjects().Value;
            Vec3 obj1 = GameObject.Transform.Position + Offset;
            Vec3 bound1 = Bounds;
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].GetHashCode() != GameObject.GetHashCode() &&
                    objects[i] is IGameObject gameObject)
                {
                    Result<Collision2D> collison = gameObject.GetComponentUnsafe<Collision2D>();
                    if (collison.IsSuccess)
                    {
                        Collision2D col = collison.Value;
                        Vec3 obj2 = gameObject.Transform.Position + col.Offset;
                        Vec3 bound2 = col.Bounds;
                        if (bound1.X <= 0 && bound1.Y <= 0 ||
                            bound2.X <= 0 && bound2.Y <= 0)
                            continue;

                        if (obj1.X < obj2.X + bound2.X &&
                            obj1.X + bound1.X > obj2.X &&
                            obj1.Y < obj2.Y + bound2.Y &&
                            obj1.Y + bound1.Y > obj2.Y)
                        {
                            if (_collisionDatas.ContainsKey(gameObject.InstanceGuid))
                            {
                                OnCollisionStay(gameObject);
                            }
                            else
                            {
                                _collisionDatas.TryAdd(gameObject.InstanceGuid, new CollisionData2D
                                {
                                    Entered = true
                                });
                                OnCollisionEnter(gameObject);
                            }
                        }
                        else
                        {
                            if (_collisionDatas.ContainsKey(gameObject.InstanceGuid))
                            {
                                _collisionDatas.TryRemove(gameObject.InstanceGuid, out _);
                                OnCollisionLeave(gameObject);
                            }
                        }
                    }
                }
            }
        }

        class CollisionData2D
        {
            public bool Entered { get; set; }
        }
    }
}
using System;
using System.Collections.Concurrent;
using EngineGL.Core;
using EngineGL.Structs.Math;

namespace EngineGL.Impl.Components
{
    public class Collision3D : Component, ICollision
    {
        private ConcurrentDictionary<Guid, CollisionData3D> _collisionDatas =
            new ConcurrentDictionary<Guid, CollisionData3D>();

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
            Vec3 obj1 = GameObject.Transform.Position;
            Vec3 bound1 = GameObject.Transform.Bounds;
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].GetHashCode() != GameObject.GetHashCode() &&
                    objects[i] is IGameObject gameObject)
                {
                    Vec3 obj2 = gameObject.Transform.Position;
                    Vec3 bound2 = gameObject.Transform.Bounds;
                    if (bound1.X <= 0 && bound1.Y <= 0 && bound1.Z <= 0 ||
                        bound2.X <= 0 && bound2.Y <= 0 && bound2.Z <= 0)
                        continue;

                    if (obj1.X < obj2.X + bound2.X &&
                        obj1.X + bound1.X > obj2.X &&
                        obj1.Y < obj2.Y + bound2.Y &&
                        obj1.Y + bound1.Y > obj2.Y &&
                        obj1.Z < obj2.Z + bound2.Z &&
                        obj1.Z + bound1.Z > obj2.Z)
                    {
                        if (_collisionDatas.ContainsKey(gameObject.InstanceGuid))
                        {
                            OnCollisionStay(gameObject);
                        }
                        else
                        {
                            _collisionDatas.TryAdd(gameObject.InstanceGuid, new CollisionData3D
                            {
                                Entered = true
                            });
                            OnCollisionEnter(gameObject);
                        }
                    }

                    if (_collisionDatas.ContainsKey(gameObject.InstanceGuid))
                    {
                        _collisionDatas.TryRemove(gameObject.InstanceGuid, out _);
                        OnCollisionLeave(gameObject);
                    }
                }
            }
        }

        class CollisionData3D
        {
            public bool Entered { get; set; }
        }
    }
}
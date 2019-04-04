using System;
using EngineGL.Core;
using EngineGL.Structs.Math;
using EngineGL.Utils;

namespace EngineGL.Impl.Components
{
    public class Collision2D : Component, ICollision
    {
        public bool Entered { get; private set; }
        public IGameObject HitGameObject { get; private set; }

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
            Vec3 obj1 = GameObject.Position;
            Vec3 bound1 = GameObject.Bounds;
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].GetHashCode() != GameObject.GetHashCode() &&
                    objects[i] is IGameObject gameObject)
                {
                    Vec3 obj2 = gameObject.Position;
                    Vec3 bound2 = gameObject.Bounds;
                    if (obj1.X < obj2.X + bound2.X &&
                        obj1.X + bound1.X > obj2.X &&
                        obj1.Y < obj2.Y + bound2.Y &&
                        obj1.Y + bound1.Y > obj2.Y)
                    {
                        if (Entered && HitGameObject.GetHashCode() == gameObject.GetHashCode())
                        {
                            OnCollisionStay(gameObject);
                        }
                        else if (!Entered && HitGameObject == null)
                        {
                            Entered = true;
                            HitGameObject = gameObject;
                            OnCollisionEnter(gameObject);
                        }

                        break;
                    }

                    if (Entered && HitGameObject.GetHashCode() == gameObject.GetHashCode())
                    {
                        Entered = false;
                        HitGameObject = null;
                        OnCollisionLeave(gameObject);
                        break;
                    }
                }
            }
        }
    }
}
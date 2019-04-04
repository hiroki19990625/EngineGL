using System;
using EngineGL.Core;
using EngineGL.Structs.Math;
using EngineGL.Utils;

namespace EngineGL.Impl.Components
{
    public class Collision2D : Component, ICollision
    {
        public bool Entered { get; private set; }

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
                    if (Math.Abs(obj1.X - obj2.X) < bound1.X / 2 + bound2.X / 2 &&
                        Math.Abs(obj1.Y - obj2.Y) < bound1.Y / 2 + bound2.Y / 2)
                    {
                        if (Entered)
                        {
                            OnCollisionStay(gameObject);
                        }
                        else
                        {
                            Entered = true;
                            OnCollisionEnter(gameObject);
                        }

                        break;
                    }

                    if (Entered)
                    {
                        Entered = false;
                        OnCollisionLeave(gameObject);
                        break;
                    }
                }
            }
        }
    }
}
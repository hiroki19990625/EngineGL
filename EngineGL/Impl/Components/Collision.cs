using System;
using EngineGL.Core;
using EngineGL.Event.LifeCycle;
using EngineGL.Structs.Math;
using EngineGL.Utils;

namespace EngineGL.Impl.Components
{
    public class Collision : Component, ICollision
    {
        public bool Entered { get; private set; }

        public SerializableAction<IGameObject> OnCollisionEnter { get; set; }
        public SerializableAction<IGameObject> OnCollisionStay { get; set; }
        public SerializableAction<IGameObject> OnCollisionLeave { get; set; }

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
                        Math.Abs(obj1.Y - obj2.Y) < bound1.Y / 2 + bound2.Y / 2 &&
                        Math.Abs(obj1.Z - obj2.Z) < bound1.Z / 2 + bound2.Z / 2)
                    {
                        if (Entered)
                        {
                            OnCollisionStay?.Action(gameObject);
                        }
                        else
                        {
                            Entered = true;
                            OnCollisionEnter?.Action(gameObject);
                        }
                    }
                    else if (Entered)
                    {
                        Entered = false;
                        OnCollisionLeave?.Action(gameObject);
                    }
                }
            }
        }
    }
}
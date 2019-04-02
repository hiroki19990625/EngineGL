using System;
using EngineGL.Core;
using EngineGL.Event.LifeCycle;
using EngineGL.Structs.Math;

namespace EngineGL.Impl.Components
{
    public class Collision : Component, ICollision
    {
        public Vec3 CollisionBound { get; set; }

        public override void OnUpdate(double deltaTime)
        {
            IScene scene = GameObject.Scene;
            IObject[] objects = scene.GetObjects().Value;
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] is IGameObject gameObject)
                {
                    
                }
            }
        }

        public bool OnCollisionEnter()
        {
            throw new NotImplementedException();
        }

        public bool OnCollisionStay()
        {
            throw new NotImplementedException();
        }

        public bool OnCollisionLeave()
        {
            throw new NotImplementedException();
        }
    }
}
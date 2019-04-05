using EngineGL.Core;
using EngineGL.Impl;
using EngineGL.Impl.Components;
using EngineGL.Structs.Math;
using EngineGL.Tests.Exec.TestObjects;
using EngineGL.Utils;
using OpenTK.Input;

namespace EngineGL.Tests.Exec.TestComponents
{
    public class PlayerComponent : Collision2D
    {
        public override void OnUpdate(double deltaTime)
        {
            base.OnUpdate(deltaTime);

            InputUpdate(deltaTime);
        }

        private void InputUpdate(double deltaTime)
        {
            KeyboardState state = Keyboard.GetState();
            float x = 0;
            float y = 0;
            if (state[Key.W])
            {
                y = 1f;
            }

            if (state[Key.A])
            {
                x = -1f;
            }

            if (state[Key.S])
            {
                y = -1f;
            }

            if (state[Key.D])
            {
                x = 1f;
            }

            GameObject.Position += new Vec3(x, y, 0) * (float) deltaTime;

            IObject[] objs = GameObject.Scene.GetObjects().Value;
            foreach (IObject o in objs)
            {
                if (o is CollisionInspector go)
                {
                    go.Pos = GameObject.Bounds.X + ":" + GameObject.Bounds.Y;
                }
            }
        }

        public override void OnCollisionEnter(IGameObject gameObject)
        {
            IObject[] objs = GameObject.Scene.GetObjects().Value;
            foreach (IObject o in objs)
            {
                if (o is CollisionInspector go)
                {
                    go.Collision = true;
                }
            }
        }

        public override void OnCollisionStay(IGameObject gameObject)
        {
            IObject[] objs = GameObject.Scene.GetObjects().Value;
            foreach (IObject o in objs)
            {
                if (o is CollisionInspector go)
                {
                    go.Collision = true;
                }
            }
        }

        public override void OnCollisionLeave(IGameObject gameObject)
        {
            IObject[] objs = GameObject.Scene.GetObjects().Value;
            foreach (IObject o in objs)
            {
                if (o is CollisionInspector go)
                {
                    go.Collision = false;
                }
            }
        }
    }
}
using System;
using EngineGL.Core;
using EngineGL.Impl.Components;
using EngineGL.Impl.Drawable.Shape2D;
using EngineGL.Structs.Math;
using OpenTK.Graphics;
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

            GameObject.Transform.Position += new Vec3(x, y, 0) * (float) deltaTime;
        }

        public override void OnCollisionEnter(IGameObject gameObject)
        {
            if (gameObject.Tag == "Text")
                ((SolidPolygonObject2D) GameObject).PoligonColor = Color4.Red;
        }

        public override void OnCollisionStay(IGameObject gameObject)
        {
        }

        public override void OnCollisionLeave(IGameObject gameObject)
        {
            if (gameObject.Tag == "Text")
                ((SolidPolygonObject2D) GameObject).PoligonColor = Color4.Yellow;
        }
    }
}
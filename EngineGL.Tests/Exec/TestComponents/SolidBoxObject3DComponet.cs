using EngineGL.Core;
using EngineGL.Impl.Components;
using EngineGL.Impl.Drawable.Shape2D;
using EngineGL.Impl.Objects;
using EngineGL.Structs.Math;
using OpenTK.Graphics;
using OpenTK.Input;

namespace EngineGL.Tests.Exec.TestComponents
{
    public class SolidBoxObject3DComponet : Collision3D

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

            if (state[Key.Up])
            {
                x = 30f;
            }

            if (state[Key.Left])
            {
                y = -30f;
            }

            if (state[Key.Down])
            {
                x = -30f;
            }

            if (state[Key.Right])
            {
                y = 30f;
            }

            GameObject.Transform.Rotation += new Vec3(x, y, 0) * (float) deltaTime;
        }
    }
}
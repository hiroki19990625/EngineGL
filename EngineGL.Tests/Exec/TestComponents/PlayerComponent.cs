using EngineGL.Impl;
using EngineGL.Structs.Math;
using OpenTK.Input;

namespace EngineGL.Tests.Exec.TestComponents
{
    public class PlayerComponent : Component
    {
        public override void OnUpdate(double deltaTime)
        {
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

            GameObject.Position += new Vec3(x, y, 0) * (float) deltaTime * 0.25f;
        }
    }
}
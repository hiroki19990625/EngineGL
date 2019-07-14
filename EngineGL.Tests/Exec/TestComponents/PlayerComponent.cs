using EngineGL.Impl.Components;
using EngineGL.Structs.Math;
using OpenTK.Input;

namespace EngineGL.Tests.Exec.TestComponents
{
    public class PlayerComponent : Component
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
            if (state[Key.Q])
            {
                Vec3 rotation = GameObject.Transform.LocalRotation;
                GameObject.Transform.LocalRotation = new Vec3(rotation.X, rotation.Y, rotation.Z + 1);
            }

            GameObject.Transform.Position += new Vec3(x, y, 0) * (float) deltaTime;
        }
    }
}
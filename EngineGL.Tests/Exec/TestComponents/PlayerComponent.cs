using EngineGL.Impl.Components;
using EngineGL.Impl.Components.Physics;
using EngineGL.Structs.Math;
using OpenTK.Input;

namespace EngineGL.Tests.Exec.TestComponents
{
    public class PlayerComponent : Component
    {
        private RigidBody3D _body;

        public override void OnInitialze()
        {
            base.OnInitialze();

            _body = GameObject.GetComponentUnsafe<RigidBody3D>().Value;
        }

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
                y = -1f;
            }

            if (state[Key.A])
            {
                x = -1f;
            }

            if (state[Key.S])
            {
                y = 1f;
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

            if (x != 0 && y != 0)
            {
                _body.RigidBody.IsActive = true;
            }

            _body.RigidBody.AddForce(new Vec3(x, 0, y) * 5f);
        }
    }
}
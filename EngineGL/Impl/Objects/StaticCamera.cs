using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Objects
{
    public class StaticCamera : Camera
    {
        public override void OnDraw(double deltaTime)
        {
            base.OnDraw(deltaTime);

            Matrix4 pos = Matrix4.CreateTranslation(Transform.Position);
            Matrix4 rotX = Matrix4.CreateRotationX(Transform.Rotation.X);
            Matrix4 rotY = Matrix4.CreateRotationY(Transform.Rotation.Y);
            Matrix4 rotZ = Matrix4.CreateRotationZ(Transform.Rotation.Z);
            Matrix4 scale = Matrix4.CreateScale(Transform.Scale);

            Matrix4 mat = LookAtMatrix * pos * rotX * rotY * rotZ * scale;

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref mat);
        }

        public override void OnUpdate(double deltaTime)
        {
            base.OnUpdate(deltaTime);

            _lookAtMatrix =
                Matrix4.LookAt(Vector3.Zero, -Vector3.UnitZ, Vector3.UnitY);
        }
    }
}
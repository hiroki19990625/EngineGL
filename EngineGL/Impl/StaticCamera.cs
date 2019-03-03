using EngineGL.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl
{
    public class StaticCamera : Camera
    {
        public override void OnDraw()
        {
            base.OnDraw();

            Matrix4 pos = Matrix4.CreateTranslation(Position);
            Matrix4 rotX = Matrix4.CreateRotationX(Rotation.X);
            Matrix4 rotY = Matrix4.CreateRotationY(Rotation.Y);
            Matrix4 rotZ = Matrix4.CreateRotationZ(Rotation.Z);
            Matrix4 scale = Matrix4.CreateScale(Scale);

            Matrix4 mat = LookAtMatrix * pos * rotX * rotY * rotZ * scale;

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref mat);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            Position += Vector3.UnitX * 0.01f;
            LookAtMatrix =
                Matrix4.LookAt(Vector3.Zero, -Vector3.UnitZ, Vector3.UnitY);
        }
    }
}
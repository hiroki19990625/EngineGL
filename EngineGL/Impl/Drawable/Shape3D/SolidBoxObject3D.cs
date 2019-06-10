using EngineGL.Structs.Math;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable.Sharpe3D
{
    public class SolidBoxObject3D : DrawableObject
    {
        public Color4 BoxColor { get; set; }

        public override void OnDraw(double deltaTime)
        {
            base.OnDraw(deltaTime);

            GL.Begin(PrimitiveType.Quads);
            GL.Color4(BoxColor);
            GL.Vertex3(Transform.Position);
            GL.Vertex3(Transform.Position + new Vec3(0, Transform.Bounds.Y, 0));
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, Transform.Bounds.Y, 0));
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, 0, 0));
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, 0, Transform.Bounds.Z));
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, Transform.Bounds.Y, Transform.Bounds.Z));
            GL.Vertex3(Transform.Position + new Vec3(0, Transform.Bounds.Y, Transform.Bounds.Z));
            GL.Vertex3(Transform.Position + new Vec3(0, 0, Transform.Bounds.Z));
            GL.End();
        }
    }
}
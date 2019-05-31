using EngineGL.Structs.Math;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable.Shape2D
{
    public class SolidBoxObject2D : DrawableObject
    {
        public Color4 BoxColor { get; set; }

        public override void OnDraw(double deltaTime)
        {
            base.OnDraw(deltaTime);

            GL.Begin(PrimitiveType.Quads);
            GL.Color4(BoxColor);
            GL.Vertex3(Position);
            GL.Vertex3(Position + new Vec3(0, Bounds.Y, Bounds.Z));
            GL.Vertex3(Position + new Vec3(Bounds.X, Bounds.Y, Bounds.Z));
            GL.Vertex3(Position + new Vec3(Bounds.X, 0, Bounds.Z));
            GL.End();
        }
    }
}
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
            GL.Vertex3(Transform.Position + new Vec3(-Transform.Bounds.X / 2, -Transform.Bounds.Y / 2));
            GL.Vertex3(Transform.Position + new Vec3(-Transform.Bounds.X / 2, Transform.Bounds.Y / 2));
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X / 2, Transform.Bounds.Y / 2, 0));
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X / 2, -Transform.Bounds.Y / 2, 0));
            GL.End();
        }
    }
}
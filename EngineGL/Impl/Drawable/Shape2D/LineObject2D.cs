using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable.Shape2D
{
    public class LineObject2D : DrawableObject
    {
        public Color4 LineColor { get; set; }
        public float LineWidth { get; set; } = 1;

        public override void OnDraw(double deltaTime)
        {
            CallDrawEvent(deltaTime);

            GL.LineWidth(LineWidth);

            GL.Begin(PrimitiveType.Lines);
            GL.Color4(LineColor);
            GL.Vertex3(Transform.Position);
            GL.Vertex3(Transform.Position + Transform.Bounds);
            GL.End();
        }
    }
}
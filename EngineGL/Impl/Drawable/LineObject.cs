using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable
{
    public class LineObject : DrawableObject
    {
        public Color4 LineColor { get; set; }
        public float LineWidth { get; set; } = 1;

        public override void OnDraw()
        {
            CallDrawEvent();

            GL.LineWidth(LineWidth);

            GL.Begin(PrimitiveType.Lines);
            GL.Color4(LineColor);
            GL.Vertex3(Position);
            GL.Vertex3(Position + Bounds);
            GL.End();
        }
    }
}
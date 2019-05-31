using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable.Shape2D
{
    public class StippleLineObject2D : LineObject2D
    {
        public byte Factor { get; set; }
        public ushort Pattern { get; set; }

        public override void OnDraw(double deltaTime)
        {
            CallDrawEvent(deltaTime);

            GL.LineWidth(LineWidth);
            GL.LineStipple(Factor, Pattern);

            GL.Enable(EnableCap.LineStipple);
            GL.Begin(PrimitiveType.Lines);
            GL.Color4(LineColor);
            GL.Vertex3(Position);
            GL.Vertex3(Position + Bounds);
            GL.End();
        }
    }
}
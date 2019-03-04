using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable
{
    public class StippleLineObject : LineObject
    {
        public byte Factor { get; set; }
        public ushort Pattern { get; set; }

        public override void OnDraw()
        {
            CallDrawEvent();

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
using EngineGL.GraphicAdapter;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable.Shape2D
{
    public class StippleLineObject2D : LineObject2D
    {
        public byte Factor { get; set; }
        public ushort Pattern { get; set; }

        public override void OnPreprocessVertex(double deltaTime, IPreprocessVertexHandler preprocessVertexHandler)
        {
            base.OnPreprocessVertex(deltaTime, preprocessVertexHandler);

            GL.LineWidth(LineWidth);
            GL.LineStipple(Factor, Pattern);
            GL.Enable(EnableCap.LineStipple);
        }

        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);

            GL.Color4(LineColor);
            GL.Vertex3(Transform.Position);
            GL.Vertex3(Transform.Position + Transform.Bounds);
        }
    }
}
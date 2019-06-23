using EngineGL.GraphicAdapter.Interface;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.DrawableComponents.Shape2D
{
    public class StippleLineObject2D : LineObject2D
    {
        public byte Factor { get; set; }
        public ushort Pattern { get; set; }

        public override void OnPreprocessVertex(double deltaTime, IPreprocessVertexHandler preprocessVertexHandler)
        {
            base.OnPreprocessVertex(deltaTime, preprocessVertexHandler);

            preprocessVertexHandler.SetLineWidth(LineWidth);
            GL.LineStipple(Factor, Pattern);
            GL.Enable(EnableCap.LineStipple);
        }
    }
}
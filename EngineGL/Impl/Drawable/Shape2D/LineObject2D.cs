using EngineGL.GraphicAdapter;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable.Shape2D
{
    public class LineObject2D : DrawableObject
    {
        public Color4 LineColor { get; set; }
        public float LineWidth { get; set; } = 1;

        public LineObject2D() : base(GraphicAdapterFactory.OpenGL1.CreateLines()) { }

        public override void OnPreprocessVertex(double deltaTime, IPreprocessVertexHandler preprocessVertexHandler)
        {
            base.OnPreprocessVertex(deltaTime, preprocessVertexHandler);
            GL.LineWidth(LineWidth);
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
using EngineGL.GraphicAdapter;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable.Shape2D
{
    public class PointsObject2D : DrawableObject
    {
        public Color4 PointColor { get; set; }
        public float PointSize { get; set; } = 1;

        public PointsObject2D() : base(GraphicAdapterFactory.OpenGL1.CreatePoints()) { }

        public override void OnPreprocessVertex(double deltaTime, IPreprocessVertexHandler preprocessVertexHandler)
        {
            base.OnPreprocessVertex(deltaTime, preprocessVertexHandler);
            GL.PointSize(PointSize);
        }

        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);

            GL.Color4(PointColor);
            GL.Vertex3(Transform.Position);
        }
    }
}
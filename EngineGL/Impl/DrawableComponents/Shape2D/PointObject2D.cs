using EngineGL.GraphicAdapter;
using EngineGL.GraphicAdapter.Interface;
using EngineGL.Structs.Math;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.DrawableComponents.Shape2D
{
    public class PointsObject2D : DrawableComponent
    {
        public float PointSize { get; set; } = 1;

        public PointsObject2D() : base(GraphicAdapterFactory.OpenGL2.CreatePoints())
        {
        }

        public override void OnPreprocessVertex(double deltaTime, IPreprocessVertexHandler preprocessVertexHandler)
        {
            base.OnPreprocessVertex(deltaTime, preprocessVertexHandler);
            GL.PointSize(PointSize);
        }

        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);

            vertexHandler.SetVertces3(new Vec3[] {Vec3.Zero});
        }
    }
}
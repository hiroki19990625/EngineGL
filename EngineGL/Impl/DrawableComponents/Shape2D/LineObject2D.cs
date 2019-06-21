using EngineGL.GraphicAdapter;
using EngineGL.GraphicAdapter.Interface;
using EngineGL.Structs.Math;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.DrawableComponents.Shape2D
{
    public class LineObject2D : DrawableComponent
    {
        public float LineWidth { get; set; } = 1;

        public LineObject2D() : base(GraphicAdapterFactory.OpenGL2.CreateLines())
        {
        }

        public override void OnPreprocessVertex(double deltaTime, IPreprocessVertexHandler preprocessVertexHandler)
        {
            base.OnPreprocessVertex(deltaTime, preprocessVertexHandler);
            GL.LineWidth(LineWidth);
        }

        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);
            vertexHandler.SetVertces3(new Vec3[]
            {
                Vec3.Zero,
                GameObject.Transform.Bounds
            });
        }
    }
}
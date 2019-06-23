using EngineGL.GraphicAdapter;
using EngineGL.GraphicAdapter.Interface;
using EngineGL.Structs.Math;

namespace EngineGL.Impl.DrawableComponents.Shape2D
{
    public class SolidBoxObject2D : DrawableComponent
    {
        public SolidBoxObject2D()
            : base(GraphicAdapterFactory.OpenGL2.CreateQuads())
        {
        }

        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);
            vertexHandler.SetVertces3(new Vec3[]
            {
                new Vec3(-GameObject.Transform.Bounds.X / 2, -GameObject.Transform.Bounds.Y / 2),
                new Vec3(GameObject.Transform.Bounds.X / 2, -GameObject.Transform.Bounds.Y / 2),
                new Vec3(GameObject.Transform.Bounds.X / 2, GameObject.Transform.Bounds.Y / 2),
                new Vec3(-GameObject.Transform.Bounds.X / 2, GameObject.Transform.Bounds.Y / 2),
            });
        }
    }
}
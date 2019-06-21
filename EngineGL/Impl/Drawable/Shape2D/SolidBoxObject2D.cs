using EngineGL.GraphicAdapter;
using EngineGL.Structs.Math;

namespace EngineGL.Impl.Drawable.Shape2D
{
    public class SolidBoxObject2D : DrawableObject
    {
        public SolidBoxObject2D()
            : base(GraphicAdapterFactory.OpenGL2.CreateQuads()) { }

        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);
            vertexHandler.SetVertces3(new Vec3[] {
               new Vec3(-Transform.Bounds.X / 2, -Transform.Bounds.Y / 2),
               new Vec3(Transform.Bounds.X / 2, -Transform.Bounds.Y / 2),
               new Vec3(Transform.Bounds.X / 2, Transform.Bounds.Y / 2),
               new Vec3(-Transform.Bounds.X / 2, Transform.Bounds.Y / 2),
            });
        }
    }
}
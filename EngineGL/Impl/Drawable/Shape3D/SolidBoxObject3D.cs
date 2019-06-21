using EngineGL.GraphicAdapter;
using EngineGL.Structs.Math;

namespace EngineGL.Impl.Drawable.Shape3D
{
    public class SolidBoxObject3D : DrawableObject
    {

        public SolidBoxObject3D() : base(GraphicAdapterFactory.OpenGL2.CreateQuads()){}

        public override void OnPreprocessVertex(double deltaTime, IPreprocessVertexHandler preprocessVertexHandler)
        {
            base.OnPreprocessVertex(deltaTime, preprocessVertexHandler);
        }
        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);

            float BX = Transform.Bounds.X;
            float BY = Transform.Bounds.Y;
            float BZ = Transform.Bounds.Z;

            vertexHandler.Indices(new uint[] {
            //正面
                0,1,2,3,
            //背面
                4,5,6,7,
            //左側面
                0,1,5,4,
            //右側面
                2,3,7,6,
            //上側面
                1,2,6,5,
            //下側面
                0,3,7,4
            });

            vertexHandler.Vertces3(new Vec3[] {
                Vec3.Zero,
                new Vec3(0, BY, 0),
                new Vec3(BX, BY, 0),
                new Vec3(BX, 0, 0),
                new Vec3(0, 0, BZ),
                new Vec3(0, BY, BZ),
                new Vec3(BX, BY, BZ),
                new Vec3(BX, 0, BZ),
            });
        }
    }
}
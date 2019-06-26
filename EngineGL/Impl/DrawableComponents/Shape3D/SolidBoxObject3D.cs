using EngineGL.GraphicAdapter;
using EngineGL.GraphicAdapter.Interface;
using EngineGL.Structs.Math;

namespace EngineGL.Impl.DrawableComponents.Shape3D
{
    public class SolidBoxObject3D : DrawableComponent
    {
        public SolidBoxObject3D() : base(GraphicAdapterFactory.OpenGL2.CreateQuads())
        {
        }

        public override void OnGraphicSetting(double deltaTime, ISettingHandler settingHandler)
        {
            base.OnGraphicSetting(deltaTime, settingHandler);
        }

        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);

            float BX = GameObject.Transform.Bounds.X;
            float BY = GameObject.Transform.Bounds.Y;
            float BZ = GameObject.Transform.Bounds.Z;

            vertexHandler.SetIndices(new uint[]
            {
                //正面
                0, 1, 2, 3,
                //背面
                4, 5, 6, 7,
                //左側面
                0, 1, 5, 4,
                //右側面
                2, 3, 7, 6,
                //上側面
                1, 2, 6, 5,
                //下側面
                0, 3, 7, 4
            });

            vertexHandler.SetVertces3(new Vec3[]
            {
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
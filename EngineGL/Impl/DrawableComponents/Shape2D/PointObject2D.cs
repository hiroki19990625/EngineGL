using EngineGL.GraphicAdapter;
using EngineGL.GraphicAdapter.Interface;
using EngineGL.Structs.Math;

namespace EngineGL.Impl.DrawableComponents.Shape2D
{
    public class PointsObject2D : DrawableComponent
    {
        public float PointSize { get; set; } = 1;

        public PointsObject2D() : base(GraphicAdapterFactory.CreatePoints())
        {
        }

        public override void OnGraphicSetting(double deltaTime, ISettingHandler settingHandler)
        {
            base.OnGraphicSetting(deltaTime, settingHandler);
            settingHandler.SetPointSize(PointSize);
        }

        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);

            vertexHandler.SetVertces3(new Vec3[] {Vec3.Zero});
        }
    }
}
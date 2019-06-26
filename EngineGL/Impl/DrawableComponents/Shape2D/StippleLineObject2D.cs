using EngineGL.GraphicAdapter.Interface;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.DrawableComponents.Shape2D
{
    public class StippleLineObject2D : LineObject2D
    {
        public byte Factor { get; set; }
        public ushort Pattern { get; set; }

        public override void OnGraphicSetting(double deltaTime, ISettingHandler settingHandler)
        {
            base.OnGraphicSetting(deltaTime, settingHandler);

            settingHandler.SetLineWidth(LineWidth);
            GL.LineStipple(Factor, Pattern);
            GL.Enable(EnableCap.LineStipple);
        }
    }
}
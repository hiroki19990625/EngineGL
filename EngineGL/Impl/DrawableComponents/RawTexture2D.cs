using EngineGL.Core.Resource;
using EngineGL.GraphicAdapter;
using EngineGL.GraphicAdapter.Interface;
using EngineGL.Impl.Resource;
using EngineGL.Structs.Math;

namespace EngineGL.Impl.DrawableComponents
{
    public class RawTexture2D : DrawableComponent
    {
        public ITexture Texture { get; set; }
        public bool AutoDispose { get; set; }

        public RawTexture2D() : base(GraphicAdapterFactory.OpenGL2.CreateQuads())
        {
        }

        public RawTexture2D(ITexture texture, bool autoDispose = false) : this()
        {
            Texture = texture;
            AutoDispose = autoDispose;
        }

        public RawTexture2D(string fileName, bool autoDispose = false) : this()
        {
            Texture = ResourceManager.LoadTexture2D(fileName);
            AutoDispose = autoDispose;
        }

        public override void OnInitialze()
        {
            if (Texture.TextureHash == 0)
            {
                Texture = ResourceManager.LoadTexture2D(Texture.FileName);
            }
        }

        public override void OnGraphicSetting(double deltaTime, ISettingHandler settingHandler)
        {
            base.OnGraphicSetting(deltaTime, settingHandler);
            settingHandler.SetTexture(Texture);
        }

        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);
            vertexHandler.SetVertces3(new Vec3[]
            {
                Vec3.Zero,
                new Vec3(0, GameObject.Transform.Bounds.Y, GameObject.Transform.Bounds.Z),
                new Vec3(GameObject.Transform.Bounds.X, GameObject.Transform.Bounds.Y, GameObject.Transform.Bounds.Z),
                new Vec3(GameObject.Transform.Bounds.X, 0, GameObject.Transform.Bounds.Z)
            });
            vertexHandler.SetUv(new Vec2[]
            {
                new Vec2(0.0f, 0.0f),
                new Vec2(0.0f, 1.0f),
                new Vec2(1.0f, 1.0f),
                new Vec2(1.0f, 0.0f)
            });
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            if (AutoDispose)
            {
                Texture.Dispose();
            }
        }
    }
}
using EngineGL.Core.Resource;
using EngineGL.Impl.Resource;
using EngineGL.Structs.Math;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable.Shape2D
{
    public class RawTexture2D : DrawableObject
    {
        public ITexture Texture { get; set; }
        public bool AutoDispose { get; set; }

        public RawTexture2D()
        {
        }

        public RawTexture2D(ITexture texture, bool autoDispose = false)
        {
            Texture = texture;
            AutoDispose = autoDispose;
        }

        public RawTexture2D(string fileName, bool autoDispose = false)
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

        public override void OnDraw(double deltaTime)
        {
            base.OnDraw(deltaTime);

            GL.BindTexture(TextureTarget.Texture2D, Texture.TextureHash);

            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord3(0.0f, 0.0f, 0.0f);
            GL.Vertex3(Position);
            GL.TexCoord3(0.0f, 1.0f, 1.0f);
            GL.Vertex3(Position + new Vec3(0, Bounds.Y, Bounds.Z));
            GL.TexCoord3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(Position + new Vec3(Bounds.X, Bounds.Y, Bounds.Z));
            GL.TexCoord3(1.0f, 0.0f, 1.0f);
            GL.Vertex3(Position + new Vec3(Bounds.X, 0, Bounds.Z));

            GL.End();
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
using EngineGL.Core.Resource;
using EngineGL.GraphicAdapter;
using EngineGL.Impl.Resource;
using EngineGL.Structs.Math;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable
{
    public class RawTexture2D : DrawableObject
    {
        public ITexture Texture { get; set; }
        public bool AutoDispose { get; set; }

        public RawTexture2D() : base(GraphicAdapterFactory.OpenGL2.CreateTriangles())
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

        public override void OnPreprocessVertex(double deltaTime, IPreprocessVertexHandler preprocessVertexHandler)
        {
            base.OnPreprocessVertex(deltaTime, preprocessVertexHandler);
            GL.BindTexture(TextureTarget.Texture2D, Texture.TextureHash);
        }

        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);

            GL.TexCoord3(0.0f, 0.0f, 0.0f);
            GL.Vertex3(Transform.Position);
            GL.TexCoord3(0.0f, 1.0f, 1.0f);
            GL.Vertex3(Transform.Position + new Vec3(0, Transform.Bounds.Y, Transform.Bounds.Z));
            GL.TexCoord3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, Transform.Bounds.Y, Transform.Bounds.Z));
            GL.TexCoord3(1.0f, 0.0f, 1.0f);
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, 0, Transform.Bounds.Z));

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
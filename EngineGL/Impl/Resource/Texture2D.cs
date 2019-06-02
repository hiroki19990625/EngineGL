using System.Drawing;
using EngineGL.Core.Resource;

namespace EngineGL.Impl.Resource
{
    public class Texture2D : ITexture
    {
        public int TextureHash { get; }

        public Bitmap Bitmap { get; }

        public Texture2D(Bitmap bitmap, int hash)
        {
            TextureHash = hash;
            Bitmap = bitmap;
        }

        public void Dispose()
        {
            Bitmap?.Dispose();
        }
    }
}
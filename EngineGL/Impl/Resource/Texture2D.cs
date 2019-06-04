using System.Drawing;
using EngineGL.Core.Resource;
using Newtonsoft.Json;

namespace EngineGL.Impl.Resource
{
    public class Texture2D : ITexture
    {
        public string FileName { get; }

        [JsonIgnore] public int TextureHash { get; }

        [JsonIgnore] public Bitmap Bitmap { get; }

        public Texture2D(string fileName, Bitmap bitmap, int hash)
        {
            FileName = fileName;
            TextureHash = hash;
            Bitmap = bitmap;
        }

        public void Dispose()
        {
            Bitmap?.Dispose();
        }
    }
}
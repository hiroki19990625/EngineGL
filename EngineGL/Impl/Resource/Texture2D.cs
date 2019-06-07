using System.Drawing;
using System.Drawing.Imaging;
using EngineGL.Core.Resource;
using Newtonsoft.Json;
using OpenTK.Graphics.OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

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

        public void Load()
        {
            Bitmap bitmap = new Bitmap(FileName);
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            int ptr = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, ptr);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int) TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int) TextureMagFilter.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Dispose()
        {
            Bitmap?.Dispose();
        }
    }
}
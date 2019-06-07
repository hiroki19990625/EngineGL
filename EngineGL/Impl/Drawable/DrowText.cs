using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable
{

    public class TextRenderer : IDisposable
    {
        Bitmap _bitmap;
        Graphics _graphics;
        int _texture;
        Rectangle _rectangle;
        bool _disposed;

        public TextRenderer(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException("width");
            if (height <= 0)
                throw new ArgumentOutOfRangeException("height");
            if (GraphicsContext.CurrentContext == null)
                throw new InvalidOperationException("No GraphicsContext is current on the calling thread.");

            _bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics.FromImage(_bitmap);
            _graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            _texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, _texture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int) TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int) TextureMagFilter.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0,
                PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
        }

        public void Clear(Color color)
        {
            _graphics.Clear(color);
            _rectangle = new Rectangle(0, 0, _bitmap.Width, _bitmap.Height);
        }

        public void DrawString(string text, Font font, Brush brush, PointF point)
        {
            _graphics.DrawString(text, font, brush, point);
            SizeF size = _graphics.MeasureString(text, font);
            _rectangle = Rectangle.Round(RectangleF.Union(_rectangle, new RectangleF(point, size)));
            _rectangle = Rectangle.Intersect(_rectangle, new Rectangle(0, 0, _bitmap.Width, _bitmap.Height));
        }

        public int Texture
        {
            get
            {
                UploadBitmap();
                return _texture;
            }
        }

        private void UploadBitmap()
        {
            if (_rectangle != Rectangle.Empty)
            {
                System.Drawing.Imaging.BitmapData data = _bitmap.LockBits(_rectangle,
                    System.Drawing.Imaging.ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                GL.BindTexture(TextureTarget.Texture1D, _texture);
                GL.TexSubImage2D(TextureTarget.Texture1D,
                    0, _rectangle.X, _rectangle.Y, _rectangle.Width, _rectangle.Height,
                    PixelFormat.Bgra,
                    PixelType.UnsignedByte,
                    data.Scan0);
                _bitmap.UnlockBits(data);
                _rectangle = Rectangle.Empty;
            }
        }

        private void Dispose(bool manual)
        {
            if (!_disposed)
            {
                _bitmap.Dispose();
                _graphics.Dispose();
                if (GraphicsContext.CurrentContext != null)
                {
                    OpenTK.Graphics.OpenGL4.GL.DeleteTexture(_texture);
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TextRenderer()
        {
            Console.WriteLine("[Warning] Resource leaked: {0}.", typeof(TextRenderer));
        }
    }
}
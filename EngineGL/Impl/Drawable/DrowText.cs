using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable
{
    class TextRendering : GameWindow
    {
        TextRenderer renderer;
        Font serif = new Font(FontFamily.GenericSerif, 24);
        Font sans = new Font(FontFamily.GenericSansSerif, 24);
        Font mono = new Font(FontFamily.GenericMonospace, 24);

        public class TextRenderer : IDisposable
        {
            Bitmap bitmap;
            Graphics graphics;
            int texture;
            Rectangle rectangle;
            bool disposed;

            public TextRenderer(int width, int height)
            {
                if (width <= 0)
                    throw new ArgumentOutOfRangeException("width");
                if (height <= 0)
                    throw new ArgumentOutOfRangeException("height");
                if (GraphicsContext.CurrentContext == null)
                    throw new InvalidOperationException("No GraphicsContext is current on the calling thread.");

                bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics.FromImage(bitmap);
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                texture = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, texture);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                    (int) TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                    (int) TextureMagFilter.Linear);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0,
                    PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
            }

            public void Clear(Color color)
            {
                graphics.Clear(color);
                rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            }

            public void DrawString(string text, Font font, Brush brush, PointF point)
            {
                graphics.DrawString(text, font, brush, point);
                SizeF size = graphics.MeasureString(text, font);
                rectangle = Rectangle.Round(RectangleF.Union(rectangle, new RectangleF(point, size)));
                rectangle = Rectangle.Intersect(rectangle, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            }

            public int Texture
            {
                get
                {
                    UploadBitmap();
                    return texture;
                }
            }

            private void UploadBitmap()
            {
                if (rectangle != Rectangle.Empty)
                {
                    System.Drawing.Imaging.BitmapData data = bitmap.LockBits(rectangle,
                        System.Drawing.Imaging.ImageLockMode.ReadOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    GL.BindTexture(TextureTarget.Texture1D, texture);
                    GL.TexSubImage2D(TextureTarget.Texture1D,
                        0, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height,
                        PixelFormat.Bgra,
                        PixelType.UnsignedByte,
                        data.Scan0);
                    bitmap.UnlockBits(data);
                    rectangle = Rectangle.Empty;
                }
            }

            private void Dispose(bool manual)
            {
                if (!disposed)
                {
                    bitmap.Dispose();
                    graphics.Dispose();
                    if (GraphicsContext.CurrentContext != null)
                    {
                        OpenTK.Graphics.OpenGL4.GL.DeleteTexture(texture);
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
}
using System;
using System.Drawing;
using EngineGL.Structs.Math;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable
{
    public class TextRenderer : DrawableObject
    {
        private Bitmap _bitmap;
        private Graphics _graphics;
        private int _texture;
        private Rectangle _rectangle;
        private string _text;

        public Color Color { get; set; }


        public string Text
        {
            get => _text;
            set
            {
                DrawString(value, new Font(FontFamily.GenericSansSerif, 24), new SolidBrush(Color), new PointF());
                UploadBitmap();
                _text = value;
            }
        }

        public TextRenderer(int width, int height)
        {
            _bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            _graphics = Graphics.FromImage(_bitmap);
            _graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            _texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, _texture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int) TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int) TextureMagFilter.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0,
                PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            _bitmap.Dispose();
            _graphics.Dispose();
            GL.DeleteTexture(_texture);
        }

        public override void OnDraw(double deltaTime)
        {
            base.OnDraw(deltaTime);

            GL.BindTexture(TextureTarget.Texture2D, _texture);

            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord3(0.0f, 0.0f, 0.0f);
            GL.Vertex3(Transform.Position);
            GL.TexCoord3(0.0f, -1.0f, 1.0f);
            GL.Vertex3(Transform.Position + new Vec3(0, Transform.Bounds.Y, Transform.Bounds.Z));
            GL.TexCoord3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, Transform.Bounds.Y, Transform.Bounds.Z));
            GL.TexCoord3(1.0f, 0.0f, 1.0f);
            GL.Vertex3(Transform.Position + new Vec3(Transform.Bounds.X, 0, Transform.Bounds.Z));

            GL.End();
        }


        private void DrawString(string text, Font font, Brush brush, PointF point)
        {
            _graphics.Clear(Color.FromArgb(0));
            _rectangle = new Rectangle(0, 0, _bitmap.Width, _bitmap.Height);
            _graphics.DrawString(text, font, brush, point);
            SizeF size = _graphics.MeasureString(text, font);
            _rectangle = Rectangle.Round(RectangleF.Union(_rectangle, new RectangleF(point, size)));
            _rectangle = Rectangle.Intersect(_rectangle, new Rectangle(0, 0, _bitmap.Width, _bitmap.Height));
        }

        private void UploadBitmap()
        {
            System.Drawing.Imaging.BitmapData data = _bitmap.LockBits(_rectangle,
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.BindTexture(TextureTarget.Texture2D, _texture);
            GL.TexSubImage2D(TextureTarget.Texture2D,
                0, _rectangle.X, _rectangle.Y, _rectangle.Width, _rectangle.Height,
                PixelFormat.Bgra,
                PixelType.UnsignedByte,
                data.Scan0);
            _bitmap.UnlockBits(data);
            _rectangle = Rectangle.Empty;
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using EngineGL.GraphicAdapter;
using EngineGL.Structs.Math;
using OpenTK.Graphics.OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace EngineGL.Impl.Drawable
{
    public class TextRenderer : DrawableObject
    {
        private Bitmap _bitmap;
        private Graphics _graphics;
        private int _texture;
        private Rectangle _rectangle;
        private string _text;

        public float FontSize { get; set; } = 16f;
        public Color FontColor { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public string Text
        {
            get => _text;
            set => SetText(value);
        }

        public TextRenderer() : base(GraphicAdapterFactory.OpenGL2.CreateTriangles())
        {
        }

        public TextRenderer(int width, int height) : this()
        {
            Init(width, height);

            Width = width;
            Height = height;
        }

        public override void OnInitialze()
        {
            base.OnInitialze();

            SetText(Text);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            _bitmap.Dispose();
            _graphics.Dispose();
            GL.DeleteTexture(_texture);
        }

        public override void OnPreprocessVertex(double deltaTime, IPreprocessVertexHandler preprocessVertexHandler)
        {
            base.OnPreprocessVertex(deltaTime, preprocessVertexHandler);
            GL.BindTexture(TextureTarget.Texture2D, _texture);
        }

        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);
            vertexHandler.Vertces3(new Vec3[] {
                Vec3.Zero,
                new Vec3(0, Transform.Bounds.Y, Transform.Bounds.Z),
                new Vec3(Transform.Bounds.X, Transform.Bounds.Y, Transform.Bounds.Z),
                new Vec3(Transform.Bounds.X, 0, Transform.Bounds.Z)
            });
            vertexHandler.Indices(new uint[] { 0, 1, 2, 2, 3, 0 });
            vertexHandler.Uv(new Vec2[] {
                new Vec2(0.0f, 0.0f),
                new Vec2(0.0f, -1.0f),
                new Vec2(1.0f, -1.0f),
                new Vec2(1.0f, 0.0f)
            });
        }

        private void SetText(string text)
        {
            if (_bitmap == null && _graphics == null)
                Init(Width, Height);

            DrawString(text, new Font(FontFamily.GenericSansSerif, FontSize), new SolidBrush(FontColor),
                new PointF());
            UploadBitmap();
            _text = text;
        }

        private void Init(int width, int height)
        {
            _bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            _graphics = Graphics.FromImage(_bitmap);
            _graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            _texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, _texture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);

            GL.BindTexture(TextureTarget.Texture2D, 0);
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
            BitmapData data = _bitmap.LockBits(_rectangle,
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            GL.BindTexture(TextureTarget.Texture2D, _texture);
            GL.TexSubImage2D(TextureTarget.Texture2D,
                0, _rectangle.X, _rectangle.Y, _rectangle.Width, _rectangle.Height,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                PixelType.UnsignedByte,
                data.Scan0);
            _bitmap.UnlockBits(data);
            _rectangle = Rectangle.Empty;
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}
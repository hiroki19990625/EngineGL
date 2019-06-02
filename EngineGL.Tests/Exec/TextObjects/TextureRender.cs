using System;
using EngineGL.Core.Resource;
using EngineGL.Impl.Drawable;
using EngineGL.Impl.Resource;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Tests.Exec.TextObjects
{
    public class TextureRender : DrawableObject
    {
        public ITexture Texture { get; private set; }

        public override void OnInitialze()
        {
            base.OnInitialze();

            Texture = ResourceManager.LoadTexture2D("Images/TestImage.png");
        }

        public override void OnDraw(double deltaTime)
        {
            CallDrawEvent(deltaTime);

            GL.Enable(EnableCap.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, Texture.TextureHash);

            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0.0f, 1.0f);
            GL.Vertex2(-1f, -1f);
            GL.TexCoord2(1.0f, 1.0f);
            GL.Vertex2(1f, -1f);
            GL.TexCoord2(1.0f, 0.0f);
            GL.Vertex2(1f, 1f);
            GL.TexCoord2(0.0f, 0.0f);
            GL.Vertex2(-1f, 1f);

            GL.End();
        }
    }
}
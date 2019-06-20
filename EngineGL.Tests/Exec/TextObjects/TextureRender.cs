using System;
using EngineGL.Core.Resource;
using EngineGL.GraphicAdapter;
using EngineGL.Impl.Drawable;
using EngineGL.Impl.Resource;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Tests.Exec.TextObjects
{
    public class TextureRender : DrawableObject
    {
        public ITexture Texture { get; private set; }

        public TextureRender() : base(GraphicAdapterFactory.OpenGL2.CreateTriangles()) { }
        public override void OnInitialze()
        {
            base.OnInitialze();

            Texture = ResourceManager.LoadTexture2D("Images/download.png");
        }


        public override void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
            base.OnVertexWrite(deltaTime, vertexHandler);

            GL.Enable(EnableCap.Texture2D);


            GL.TexCoord2(0.0f, 1.0f);
            GL.Vertex2(-1f, -1f);
            GL.TexCoord2(1.0f, 1.0f);
            GL.Vertex2(1f, -1f);
            GL.TexCoord2(1.0f, 0.0f);
            GL.Vertex2(1f, 1f);
            GL.TexCoord2(0.0f, 0.0f);
            GL.Vertex2(-1f, 1f);

        }
    }
}
using EngineGL.Core.Resource;
using EngineGL.GraphicAdapter.Interface;
using EngineGL.Structs.Drawing;
using EngineGL.Structs.Math;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.GraphicAdapter.Impl.OpenGL2
{
    class PreprocessVertexHandler : IPreprocessVertexHandler
    {
        public void Euler(Vec3 angles)
        {
            GL.Rotate(angles.Y, 0, 1, 0);
            GL.Rotate(angles.Z, 0, 0, 1);
            GL.Rotate(angles.X, 1, 0, 0);
        }

        public void Rotate(float angle, float x, float y, float z)
            => GL.Rotate(angle, x, y, z);

        public void SetColour(Colour4 colour4)
            => GL.Color4(colour4);

        public void SetLineWidth(float lineWidth)
            => GL.LineWidth(lineWidth);

        public void SetPointSize(float PointSize)
            => GL.PointSize(PointSize);

        public void SetTexture(ITexture texture)
            => GL.BindTexture(TextureTarget.Texture2D, texture.TextureHash);

        public void Translate(Vec3 vec)
            => GL.Translate(vec);
    }
}

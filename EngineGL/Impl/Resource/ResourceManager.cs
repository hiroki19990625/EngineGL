using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using EngineGL.Core.Resource;
using OpenTK.Graphics.OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace EngineGL.Impl.Resource
{
    public static class ResourceManager
    {
        public static T LoadTexture<T>(string filePath, params object[] args) where T : ITexture
        {
            T texture = (T) Activator.CreateInstance(typeof(T), args);
            texture.Load();

            return texture;
        }

        public static void UnloadTexture(ITexture texture)
        {
            GL.DeleteTexture(texture.TextureHash);
            texture.Dispose();
        }
    }
}
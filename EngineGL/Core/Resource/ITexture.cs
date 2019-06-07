using System;

namespace EngineGL.Core.Resource
{
    public interface ITexture : IDisposable
    {
        string FileName { get; }
        int TextureHash { get; }

        void Load();
    }
}
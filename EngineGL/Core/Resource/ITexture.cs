using System;

namespace EngineGL.Core.Resource
{
    public interface ITexture : IDisposable
    {
        int TextureHash { get; }
    }
}
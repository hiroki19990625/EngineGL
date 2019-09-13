using System;
using EngineGL.GraphicEngine;

namespace EngineGL.Resource
{
    public class Texture : IResourceHandle
    {
        private IGraphicAdapter _adapter;

        public bool AutoDispose
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public Guid HandleId { get; } = Guid.NewGuid();

        public IRefCounter RefCounter => throw new NotImplementedException();

        public string FilePath { get; }
        public int TextureId { get; }

        public Texture(IGraphicAdapter adapter, string path)
        {
            FilePath = path;
            _adapter = adapter;

            TextureId = _adapter.LoadTexture(this);
        }

        public void Dispose()
        {
            _adapter.UnloadTexture(this);
        }
    }
}
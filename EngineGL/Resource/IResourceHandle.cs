using System;

namespace EngineGL.Resource
{
    public interface IResourceHandle : IDisposable
    {
        bool AutoDispose { get; set; }
        Guid HandleId { get; }

        IRefCounter RefCounter { get; }
    }
}
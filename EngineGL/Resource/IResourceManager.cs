using System;

namespace EngineGL.Resource
{
    public interface IResourceManager
    {
        IResourceHandle GetResourceHandle(Guid guid);
        void RegisterResourceHandle(IResourceHandle handle);
        void DisposeResourceHandle(IResourceHandle handle);

        void UpdateResourceHandles();
    }
}
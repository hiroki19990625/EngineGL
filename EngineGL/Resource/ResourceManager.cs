using System;
using System.Collections.Generic;

namespace EngineGL.Resource
{
    public class ResourceManager : IResourceManager
    {
        private Dictionary<Guid, IResourceHandle> _handles = new Dictionary<Guid, IResourceHandle>();

        public IResourceHandle GetResourceHandle(Guid guid)
        {
            IResourceHandle handle = _handles[guid];
            return handle;
        }

        public void RegisterResourceHandle(IResourceHandle handle)
        {
            _handles[handle.HandleId] = handle;
        }

        public void DisposeResourceHandle(IResourceHandle handle)
        {
            handle.Dispose();
            _handles.Remove(handle.HandleId);
        }

        public void UpdateResourceHandles()
        {
            //TODO: Check RefCounter
        }
    }
}
using System;
using EngineGL.Event.LifeCycle;

namespace EngineGL.Core.LifeCycle
{
    public interface IDestroyable
    {
        event EventHandler<DestroyEventArgs> Destroy;

        void OnDestroy();
    }
}
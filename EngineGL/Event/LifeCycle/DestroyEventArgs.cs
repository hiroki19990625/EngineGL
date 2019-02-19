using System;
using EngineGL.Core.LifeCycle;

namespace EngineGL.Event.LifeCycle
{
    public class DestroyEventArgs : EventArgs
    {
        public IDestroyable DestroyTarget { get; set; }

        public DestroyEventArgs(IDestroyable destroyable)
        {
            DestroyTarget = destroyable;
        }
    }
}
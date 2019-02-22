using System;
using EngineGL.Core;

namespace EngineGL.Event.ComponentAttachable
{
    public class ComponentAttachableEventArgs : EventArgs
    {
        public IComponentAttachable ComponentAttachable { get; }

        public ComponentAttachableEventArgs(IComponentAttachable componentAttachable)
        {
            ComponentAttachable = componentAttachable;
        }
    }
}
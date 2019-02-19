using System;
using EngineGL.Core;

namespace EngineGL.Event.Component
{
    public class ComponentEventArgs : EventArgs
    {
        public IComponent Component { get; }

        public ComponentEventArgs(IComponent component)
        {
            Component = component;
        }
    }
}
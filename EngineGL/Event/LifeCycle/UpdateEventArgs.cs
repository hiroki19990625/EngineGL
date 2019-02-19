using System;
using EngineGL.Core.LifeCycle;

namespace EngineGL.Event.LifeCycle
{
    public class UpdateEventArgs : EventArgs
    {
        public IUpdateable UpdateTarget { get; set; }

        public UpdateEventArgs(IUpdateable updateable)
        {
            UpdateTarget = updateable;
        }
    }
}
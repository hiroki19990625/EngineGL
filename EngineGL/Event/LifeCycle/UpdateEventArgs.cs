using System;
using EngineGL.Core.LifeCycle;

namespace EngineGL.Event.LifeCycle
{
    public class UpdateEventArgs : EventArgs
    {
        public IUpdateable UpdateTarget { get; set; }
        public double DeltaTime { get; }

        public UpdateEventArgs(IUpdateable updateable, double deltaTime)
        {
            UpdateTarget = updateable;
            DeltaTime = deltaTime;
        }
    }
}
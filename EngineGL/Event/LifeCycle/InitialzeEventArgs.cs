using System;
using EngineGL.Core.LifeCycle;

namespace EngineGL.Event.LifeCycle
{
    public class InitialzeEventArgs : EventArgs
    {
        public Initialzeable InitialzeTarget { get; set; }

        public InitialzeEventArgs(Initialzeable initialzeable)
        {
            InitialzeTarget = initialzeable;
        }
    }
}
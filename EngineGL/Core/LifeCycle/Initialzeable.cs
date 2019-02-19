using System;
using EngineGL.Event.LifeCycle;

namespace EngineGL.Core.LifeCycle
{
    public interface Initialzeable
    {
        event EventHandler<InitialzeEventArgs> Initialze;

        void OnInitialze();
    }
}
using System;
using EngineGL.Event.LifeCycle;

namespace EngineGL.Core.LifeCycle
{
    public interface IUpdateable
    {
        event EventHandler<UpdateEventArgs> Update;

        void OnUpdate(double deltaTime);
    }
}
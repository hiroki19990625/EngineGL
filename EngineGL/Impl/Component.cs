using System;
using EngineGL.Core;
using EngineGL.Event.LifeCycle;

namespace EngineGL.Impl
{
    public class Component : IComponent
    {
        public event EventHandler<InitialzeEventArgs> Initialze;
        public event EventHandler<UpdateEventArgs> Update;
        public event EventHandler<DestroyEventArgs> Destroy;

        public virtual void OnInitialze()
        {
            Initialze?.Invoke(this, new InitialzeEventArgs(this));
        }

        public virtual void OnUpdate()
        {
            Update?.Invoke(this, new UpdateEventArgs(this));
        }

        public virtual void OnDestroy()
        {
            Destroy?.Invoke(this, new DestroyEventArgs(this));
        }
    }
}
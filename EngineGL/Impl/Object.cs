using System;
using EngineGL.Core;
using EngineGL.Event.LifeCycle;

namespace EngineGL.Impl
{
    public class Object : IObject
    {
        public string Name { get; set; }
        public string Tag { get; set; }

        public event EventHandler<InitialzeEventArgs> Initialze;

        public virtual void OnInitialze()
        {
            Initialze?.Invoke(this, new InitialzeEventArgs(this));
        }

        public event EventHandler<DestroyEventArgs> Destroy;

        public virtual void OnDestroy()
        {
            Destroy?.Invoke(this, new DestroyEventArgs(this));
        }

        public event EventHandler<UpdateEventArgs> Update;

        public virtual void OnUpdate()
        {
            Update?.Invoke(this, new UpdateEventArgs(this));
        }
    }
}
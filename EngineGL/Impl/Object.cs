using System;
using EngineGL.Core;
using EngineGL.Event.LifeCycle;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace EngineGL.Impl
{
    public class Object : IObject
    {
        [YamlIgnore, JsonIgnore] public IScene Scene { get; set; }

        public string Name { get; set; }
        public string Tag { get; set; }

        public event EventHandler<InitialzeEventArgs> Initialze;
        public event EventHandler<DestroyEventArgs> Destroy;
        public event EventHandler<UpdateEventArgs> Update;

        public virtual void OnInitialze()
        {
            Initialze?.Invoke(this, new InitialzeEventArgs(this));
        }

        public virtual void OnDestroy()
        {
            Destroy?.Invoke(this, new DestroyEventArgs(this));
        }

        public virtual void OnUpdate(double deltaTime)
        {
            Update?.Invoke(this, new UpdateEventArgs(this, deltaTime));
        }
    }
}
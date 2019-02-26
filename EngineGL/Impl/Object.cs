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
        public void OnInitialze()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<DestroyEventArgs> Destroy;
        public void OnDestroy()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<UpdateEventArgs> Update;
        public bool OnUpdate()
        {
            throw new NotImplementedException();
        }
    }
}
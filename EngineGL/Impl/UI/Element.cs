using System;
using EngineGL.Core;
using EngineGL.Core.Components;
using EngineGL.Event.LifeCycle;
using EngineGL.Structs.Drawing;
using Newtonsoft.Json.Linq;

namespace EngineGL.Impl.UI
{
    public class Element : IElement
    {
        public event EventHandler<InitialzeEventArgs> Initialze;

        public void OnInitialze()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<UpdateEventArgs> Update;

        public void OnUpdate(double deltaTime)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<DestroyEventArgs> Destroy;

        public void OnDestroy()
        {
            throw new NotImplementedException();
        }

        public void OnComponentInitialized()
        {
            throw new NotImplementedException();
        }

        public void OnSerialize()
        {
            throw new NotImplementedException();
        }

        public void OnDeserialize<T>(T data)
        {
            throw new NotImplementedException();
        }

        public JObject OnSerializeJson()
        {
            throw new NotImplementedException();
        }

        public void OnDeserializeJson(JObject obj)
        {
            throw new NotImplementedException();
        }

        public IComponentAttachable ParentObject { get; set; }
        public IGameObject GameObject { get; }
        public Guid InstanceGuid { get; }
        public event EventHandler<DrawEventArgs> Draw;

        public void OnDraw(double deltaTime)
        {
            throw new NotImplementedException();
        }

        public uint Layer { get; }
        public Colour4 Colour { get; set; }
    }
}
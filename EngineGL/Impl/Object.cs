using System;
using EngineGL.Core;
using EngineGL.Event.LifeCycle;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YamlDotNet.Serialization;

namespace EngineGL.Impl
{
    public class Object : IObject
    {
        [YamlIgnore, JsonIgnore] public IScene Scene { get; set; }

        public string Name { get; set; }
        public string Tag { get; set; }
        public Guid InstanceGuid { get; set; } = Guid.NewGuid();

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

        public void OnSerialize()
        {
            throw new NotImplementedException();
        }

        public void OnDeserialize<T>(T data)
        {
            throw new NotImplementedException();
        }

        public virtual JObject OnSerializeJson()
        {
            JObject cls = new JObject();
            cls["name"] = new JValue(Name);
            cls["tag"] = new JValue(Tag);
            cls["guid"] = new JValue(InstanceGuid);
            cls["type"] = new JValue(this.GetType().FullName);

            return cls;
        }

        public virtual void OnDeserializeJson(JObject obj)
        {
            Name = obj["name"].Value<string>();
            Tag = obj["tag"].Value<string>();
            InstanceGuid = obj["guid"].Value<Guid>();
        }
    }
}
using System;
using System.Reflection;
using EngineGL.Core;
using EngineGL.Core.Components;
using EngineGL.Event.LifeCycle;
using EngineGL.Serializations.Resulter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EngineGL.Impl.Components
{
    public class Component : IComponent
    {
        public event EventHandler<InitialzeEventArgs> Initialze;
        public event EventHandler<UpdateEventArgs> Update;
        public event EventHandler<DestroyEventArgs> Destroy;

        private IComponentAttachable _parentObject;

        [SerializeIgnore, JsonIgnore]
        public virtual IComponentAttachable ParentObject
        {
            get => _parentObject;
            set
            {
                if (_parentObject == null)
                    _parentObject = value;
                else
                    throw new InvalidOperationException("Component already has parent object linked.");
            }
        }

        [SerializeIgnore, JsonIgnore]
        public virtual IGameObject GameObject
        {
            get => (IGameObject) _parentObject;
        }

        public Guid InstanceGuid { get; set; } = Guid.NewGuid();

        public virtual void OnInitialze()
        {
            Initialze?.Invoke(this, new InitialzeEventArgs(this));
        }

        public virtual void OnUpdate(double deltaTime)
        {
            Update?.Invoke(this, new UpdateEventArgs(this, deltaTime));
        }

        public virtual void OnDestroy()
        {
            Destroy?.Invoke(this, new DestroyEventArgs(this));
        }

        public virtual void OnComponentInitialized()
        {
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
            cls["guid"] = new JValue(InstanceGuid);
            cls["type"] = new JValue(this.GetType().FullName);
            cls["assembly"] = new JValue(this.GetType().Assembly.GetName().Name);

            foreach (PropertyInfo property in GetType().GetProperties())
            {
                if (property.GetCustomAttribute<SerializeIgnoreAttribute>() == null)
                {
                    if (property.Name == nameof(InstanceGuid))
                        continue;

                    object data = property.GetValue(this);
                    if (data != null)
                    {
                        cls[property.Name] = JToken.FromObject(data);
                        cls[property.Name + "_type"] = new JValue(data.GetType().FullName);
                        cls[property.Name + "_assembly"] = new JValue(data.GetType().Assembly.GetName().Name);
                    }
                }
            }

            return cls;
        }

        public virtual void OnDeserializeJson(JObject obj)
        {
            InstanceGuid = new Guid(obj["guid"].Value<string>());

            foreach (PropertyInfo property in GetType().GetProperties())
            {
                if (property.GetCustomAttribute<SerializeIgnoreAttribute>() == null)
                {
                    if (property.Name == nameof(InstanceGuid))
                        continue;

                    if (obj.ContainsKey(property.Name))
                    {
                        Type type = Type.GetType(Assembly.CreateQualifiedName(
                            obj[property.Name + "_assembly"].Value<string>(),
                            obj[property.Name + "_type"].Value<string>()));

                        if (type == null)
                            continue;

                        object data = obj[property.Name].ToObject(type);
                        if (data == null)
                            continue;

                        try
                        {
                            property.SetValue(this, data);
                        }
                        catch
                        {
                            this.GetType().GetField($"<{property.Name}>k__BackingField",
                                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?
                                .SetValue(this, data);
                        }
                    }
                }
            }
        }
    }
}
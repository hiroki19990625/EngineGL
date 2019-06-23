using System;
using System.Reflection;
using EngineGL.Core;
using EngineGL.Event.LifeCycle;
using EngineGL.Serializations.Resulter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EngineGL.Impl
{
    public class Object : IObject
    {
        [SerializeIgnore, JsonIgnore] public IScene Scene { get; set; }

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
            cls["assembly"] = new JValue(this.GetType().Assembly.GetName().Name);

            foreach (PropertyInfo property in GetType().GetProperties())
            {
                if (property.GetCustomAttribute<SerializeIgnoreAttribute>() == null)
                {
                    if (property.Name == nameof(InstanceGuid) || property.Name == nameof(Name) ||
                        property.Name == nameof(Tag))
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
            Name = obj["name"].Value<string>();
            Tag = obj["tag"].Value<string>();
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
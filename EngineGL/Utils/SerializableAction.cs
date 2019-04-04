using System;
using System.Reflection;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace EngineGL.Utils
{
    public class SerializableAction
    {
        [JsonIgnore, YamlIgnore]
        public Action Action
        {
            get => Delegate.CreateDelegate(typeof(Action), Target, MethodName) as Action;

            set
            {
                if (value != null)
                {
                    Target = value.Target;
                    MethodName = value.Method.Name;
                }
            }
        }

        public object Target { get; set; }
        public string MethodName { get; set; }
    }

    public class SerializableAction<T>
    {
        [JsonIgnore, YamlIgnore]
        public Action<T> Action
        {
            get => Delegate.CreateDelegate(typeof(Action<T>), Target, MethodName) as Action<T>;

            set
            {
                if (value != null)
                {
                    Target = value.Target;
                    MethodName = value.Method.Name;
                }
            }
        }

        public object Target { get; set; }
        public string MethodName { get; set; }
    }
}
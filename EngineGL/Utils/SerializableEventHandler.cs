using System;

namespace EngineGL.Utils
{
    public class SerializableEventHandler<T> where T : EventArgs
    {
        public EventHandler<T> EventHandler
        {
            get => Delegate.CreateDelegate(typeof(EventHandler<T>), Target, MethodName) as EventHandler<T>;

            set
            {
                if (value != null)
                {
                    Target = value.Target;
                    MethodName = value.Method.Name;
                }
            }
        }

        public object Target { get; private set; }
        public string MethodName { get; private set; }
    }
}
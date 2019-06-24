using System;

namespace EngineGL.Utils
{
    public class EventManager<T> where T : EventArgs
    {
        public EventHandler<T> Handler { get; }
        public object Sender { get; }
        public T EventArgs { get; }

        public EventManager(EventHandler<T> handler, object sender, T args)
        {
            Handler = handler;
            Sender = sender;
            EventArgs = args;
        }

        public void Call()
        {
            Handler?.Invoke(Sender, EventArgs);
        }
    }
}
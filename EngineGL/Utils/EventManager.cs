using System;
using EngineGL.Event;

namespace EngineGL.Utils
{
    public class EventManager<T> where T : CancelableEvent
    {
        public EventHandler<T> Handler { get; }
        public object Sender { get; }
        public T EventArgs { get; }

        private Func<T, bool> _success = arg => false;

        public Func<T, bool> OnSuccess
        {
            get => _success;
            set
            {
                if (value != null) _success = value;
            }
        }

        private Func<T, bool> _fail = arg => false;

        public Func<T, bool> OnFail
        {
            get => _fail;
            set
            {
                if (value != null) _fail = value;
            }
        }

        public EventManager(EventHandler<T> handler, object sender, T args)
        {
            Handler = handler;
            Sender = sender;
            EventArgs = args;
        }

        public bool Call()
        {
            Handler?.Invoke(Sender, EventArgs);

            if (!EventArgs.IsCanceled)
                return OnSuccess.Invoke(EventArgs);
            else
                return OnFail.Invoke(EventArgs);
        }
    }
}
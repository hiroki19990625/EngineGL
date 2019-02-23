using System;
using EngineGL.Event;

namespace EngineGL.Core.Utils
{
    public static class EventManager
    {
        public static void Call<T>(EventHandler<T> handler, object sender, T e, Action<T> success, Action<T> cancel) where T : CancelableEvent
        {
            handler?.Invoke(sender, e);
            
            if (e.IsCanceled)
            {
                cancel.Invoke(e);
            }
            else
            {
                success.Invoke(e);
            }
        }
    }
}
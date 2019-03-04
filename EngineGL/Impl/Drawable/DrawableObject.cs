using System;
using EngineGL.Core.LifeCycle;
using EngineGL.Event.LifeCycle;

namespace EngineGL.Impl.Drawable
{
    public abstract class DrawableObject : GameObject, IDrawable
    {
        public event EventHandler<DrawEventArgs> Draw;

        public virtual void OnDraw()
        {
            CallDrawEvent();
        }

        protected void CallDrawEvent()
        {
            Draw?.Invoke(this, new DrawEventArgs(this));
        }
    }
}
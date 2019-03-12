using System;
using EngineGL.Core.LifeCycle;
using EngineGL.Event.LifeCycle;

namespace EngineGL.Impl.Drawable
{
    public abstract class DrawableObject : GameObject, IDrawable
    {
        public event EventHandler<DrawEventArgs> Draw;

        public virtual void OnDraw(double deltaTime)
        {
            CallDrawEvent(deltaTime);
        }

        protected void CallDrawEvent(double deltaTime)
        {
            Draw?.Invoke(this, new DrawEventArgs(this, deltaTime));
        }
    }
}
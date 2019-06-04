using System;
using EngineGL.Core.LifeCycle;
using EngineGL.Event.LifeCycle;

namespace EngineGL.Impl.Drawable
{
    public abstract class DrawableObject : GameObject, IDrawable
    {

        /// <summary>
        /// 描画レイヤー
        /// </summary>
        public uint Layer { get; set; } = 0;

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
using System;
using EngineGL.Event.LifeCycle;

namespace EngineGL.Core.LifeCycle
{
    public interface IDrawable
    {
        event EventHandler<DrawEventArgs> Draw;

        void OnDraw(double deltaTime);
    }
}
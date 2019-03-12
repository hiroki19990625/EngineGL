using System;
using EngineGL.Core.LifeCycle;

namespace EngineGL.Event.LifeCycle
{
    public class DrawEventArgs : EventArgs
    {
        public IDrawable DrawableTarget { get; }
        public double DeltaTime { get; }

        public DrawEventArgs(IDrawable drawable, double deltaTime)
        {
            DrawableTarget = drawable;
            DeltaTime = deltaTime;
        }
    }
}
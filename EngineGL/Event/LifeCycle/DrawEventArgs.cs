using System;
using EngineGL.Core.LifeCycle;

namespace EngineGL.Event.LifeCycle
{
    public class DrawEventArgs : EventArgs
    {
        public IDrawable DrawableTarget { get; set; }

        public DrawEventArgs(IDrawable drawable)
        {
            DrawableTarget = drawable;
        }
    }
}
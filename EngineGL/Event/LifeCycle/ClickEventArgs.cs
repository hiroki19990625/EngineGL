using System;
using EngineGL.Core.LifeCycle;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace EngineGL.Event.LifeCycle
{
    public class ClickEventArgs : EventArgs
    {
        public IClickable ClickTarget { get; }

        public ClickEventArgs(IClickable clickable)
        {
            ClickTarget = clickable;
        }
    }
}
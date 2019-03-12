using System;
using EngineGL.Core.LifeCycle;

namespace EngineGL.Event.LifeCycle
{
    public class GuiRenderEventArgs : EventArgs
    {
        public IGuiRenderable GuiRenderableTarget { get; }
        public double DeltaTime { get; }

        public GuiRenderEventArgs(IGuiRenderable guiRenderable, double deltaTime)
        {
            GuiRenderableTarget = guiRenderable;
            DeltaTime = deltaTime;
        }
    }
}
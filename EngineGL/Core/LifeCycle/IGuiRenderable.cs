using System;
using EngineGL.Event.LifeCycle;

namespace EngineGL.Core.LifeCycle
{
    public interface IGuiRenderable
    {
        event EventHandler<GuiRenderEventArgs> GuiRender;

        void OnGUI(double deltaTime);
    }
}
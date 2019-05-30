using System;
using EngineGL.Core.LifeCycle;
using EngineGL.Event.LifeCycle;

namespace EngineGL.Impl.Objects
{
    [Obsolete]
    public class GUIRender : GameObject, IGuiRenderable
    {
        public event EventHandler<GuiRenderEventArgs> GuiRender;

        public virtual void OnGUI(double deltaTime)
        {
            GuiRender?.Invoke(this, new GuiRenderEventArgs(this, deltaTime));
        }
    }
}
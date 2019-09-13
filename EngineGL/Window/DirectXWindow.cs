using System;
using System.Drawing;
using OpenTK;

namespace EngineGL.Window
{
    public class DirectXWindow : IWindow
    {
        public Icon Icon { get; set; }
        public string Title { get; set; }

        public DirectXWindow()
        {
        }

        public void AddLoadEvent(EventHandler<EventArgs> handler)
        {
            throw new NotImplementedException();
        }

        public void RemoveLoadEvent(EventHandler<EventArgs> handler)
        {
            throw new NotImplementedException();
        }

        public void AddUnloadEvent(EventHandler<EventArgs> handler)
        {
            throw new NotImplementedException();
        }

        public void RemoveUnloadEvent(EventHandler<EventArgs> handler)
        {
            throw new NotImplementedException();
        }

        public void AddRenderFrameEvent(EventHandler<FrameEventArgs> handler)
        {
            throw new NotImplementedException();
        }

        public void RemoveRenderFrameEvent(EventHandler<FrameEventArgs> handler)
        {
            throw new NotImplementedException();
        }

        public void AddUpdateFrameEvent(EventHandler<FrameEventArgs> handler)
        {
            throw new NotImplementedException();
        }

        public void RemoveUpdateFrameEvent(EventHandler<FrameEventArgs> handler)
        {
            throw new NotImplementedException();
        }
    }
}
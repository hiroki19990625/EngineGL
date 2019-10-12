using System;
using System.Drawing;
using EngineGL.GraphicEngine;
using OpenTK;

namespace EngineGL.Window
{
    public class DirectXWindow : IWindow
    {
        private IGraphicAdapter _adapter;

        public IGraphicAdapter GraphicAdapter => _adapter;

        public Icon Icon { get; set; }
        public string Title { get; set; }

        public DirectXWindow()
        {
            _adapter = GraphicFactory.CreateDirectXAdapter();
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

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
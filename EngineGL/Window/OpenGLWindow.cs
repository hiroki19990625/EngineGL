using System;
using System.Drawing;
using OpenTK;

namespace EngineGL.Window
{
    public class OpenGLWindow : IWindow
    {
        private GameWindow _window;

        public Icon Icon
        {
            get => _window.Icon;
            set => _window.Icon = value;
        }

        public string Title
        {
            get => _window.Title;
            set => _window.Title = value;
        }

        public OpenGLWindow()
        {
            _window = new GameWindow();
        }

        public void AddLoadEvent(EventHandler<EventArgs> handler)
        {
            _window.Load += handler;
        }

        public void RemoveLoadEvent(EventHandler<EventArgs> handler)
        {
            _window.Load -= handler;
        }

        public void AddUnloadEvent(EventHandler<EventArgs> handler)
        {
            _window.Unload += handler;
        }

        public void RemoveUnloadEvent(EventHandler<EventArgs> handler)
        {
            _window.Unload -= handler;
        }

        public void AddRenderFrameEvent(EventHandler<FrameEventArgs> handler)
        {
            _window.RenderFrame += handler;
        }

        public void RemoveRenderFrameEvent(EventHandler<FrameEventArgs> handler)
        {
            _window.RenderFrame -= handler;
        }

        public void AddUpdateFrameEvent(EventHandler<FrameEventArgs> handler)
        {
            _window.UpdateFrame += handler;
        }

        public void RemoveUpdateFrameEvent(EventHandler<FrameEventArgs> handler)
        {
            _window.UpdateFrame -= handler;
        }
    }
}
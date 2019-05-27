using System;
using EngineGL.Editor.Core.Control.Window;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Impl.Controls.Window
{
    public class MyDockContent : DockContent, IWindow
    {
        protected IMainWindow _hostWindow;
        public Guid WindowGuid { get; }

        public MyDockContent()
        {
        }

        public MyDockContent(IMainWindow hostWindow)
        {
            WindowGuid = Guid.NewGuid();

            _hostWindow = hostWindow;
            _hostWindow.AddWindow(this);
        }

        protected override void OnClosed(EventArgs e)
        {
            _hostWindow.RemoveWindow(this);
        }
    }
}
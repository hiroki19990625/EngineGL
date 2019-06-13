using System;
using Gtk;

namespace EngineGL.Impl.Resource
{
    public class WindowDialog : Window
    {
        public WindowDialog() : base("About")
        {
        }

        public void OpenDialog(string message)
        {
            MessageDialog md = new MessageDialog(this,
                DialogFlags.DestroyWithParent, MessageType.Info,
                ButtonsType.Close, message);
            md.Run();
            md.Destroy();
        }
    }
}
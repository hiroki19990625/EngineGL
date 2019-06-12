using System;
using Gtk;

namespace EngineGL.Impl.Resource
{
    public class WindowDialog : Window
    {
        public WindowDialog(string message) : base("About")
        {
            SetDefaultSize(250, 100);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { Application.Quit(); };


            Table table = new Table(2, 2, true);

            Button info = new Button("Information");

            info.Clicked += delegate
            {
                MessageDialog md = new MessageDialog(this,
                    DialogFlags.DestroyWithParent, MessageType.Info,
                    ButtonsType.Close, message);
                md.Run();
                md.Destroy();
            };

            table.Attach(info, 0, 1, 0, 1);

            Add(table);

            ShowAll();
        }
    }
}
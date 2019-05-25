using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Forms;
using EngineGL.Editor.Impl.Controls.Window;
using EngineGL.Utils;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Core.Control.Window
{
    public interface IMainWindow
    {
        MenuStrip MenuStrip { get; }
        ToolStrip ToolStrip { get; }
        StatusStrip StatusStrip { get; }

        DockPanel DockPanel { get; }

        void AddWindow(MyDockContent content);
        void RemoveWindow(MyDockContent content);
        Result<MyDockContent> GetWindow(Guid guid);
        Result<T> GetWindow<T>(Guid guid) where T : MyDockContent;
    }
}
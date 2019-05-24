using System;
using System.Windows.Forms;
using EngineGL.Editor.Core.Window;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Controls.Window
{
    public partial class ProjectWindow : DockContent, IWindow
    {
        public ProjectWindow()
        {
            InitializeComponent();
        }

        public Guid WindowId { get; }

        public void WindowToolStrip(ToolStrip toolStrip)
        {
            throw new NotImplementedException();
        }
    }
}
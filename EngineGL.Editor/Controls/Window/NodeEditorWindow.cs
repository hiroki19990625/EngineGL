using System;
using System.Windows.Forms;
using EngineGL.Editor.Core.Window;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Controls.Window
{
    public partial class NodeEditorWindow : DockContent, IWindow
    {
        public NodeEditorWindow()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            DockAreas = DockAreas.Document | DockAreas.Float;
        }

        public Guid WindowId { get; }
        public void WindowToolStrip(ToolStrip toolStrip)
        {
            throw new NotImplementedException();
        }
    }
}
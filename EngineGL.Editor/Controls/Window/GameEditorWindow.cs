using System;
using System.Windows.Forms;
using EngineGL.Editor.Core.Window;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Controls.Window
{
    public partial class GameEditorWindow : DockContent, IWindow
    {
        public Guid WindowId { get; }

        public GameEditorWindow()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            DockAreas = DockAreas.Document | DockAreas.Float;
        }

        public void WindowToolStrip(ToolStrip toolStrip)
        {
            throw new NotImplementedException();
        }
    }
}
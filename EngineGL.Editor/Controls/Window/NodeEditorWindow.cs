using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Controls.Window
{
    public partial class NodeEditorWindow : DockContent
    {
        public NodeEditorWindow()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            DockAreas = DockAreas.Document | DockAreas.Float;
        }
    }
}
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Controls.Window
{
    public partial class GameEditorWindow : DockContent
    {
        public GameEditorWindow()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            DockAreas = DockAreas.Document | DockAreas.Float;
        }
    }
}
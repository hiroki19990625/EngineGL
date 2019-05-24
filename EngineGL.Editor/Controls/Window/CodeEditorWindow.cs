using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using RoslynPad.Editor;
using RoslynPad.Roslyn;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Controls.Window
{
    public partial class CodeEditorWindow : DockContent
    {
        private RoslynHost _host;
        private RoslynCodeEditor _editor;

        public CodeEditorWindow()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            DockAreas = DockAreas.Document | DockAreas.Float;
        }

        private void CodeEditorWindow_Load(object sender, EventArgs e)
        {
            _host = new RoslynHost(additionalAssemblies: new[]
            {
                Assembly.Load("RoslynPad.Roslyn.Windows"),
                Assembly.Load("RoslynPad.Editor.Windows"),
            });
            _editor = new RoslynCodeEditor();
            _editor.Initialize(_host, new ClassificationHighlightColors(), Directory.GetCurrentDirectory(),
                string.Empty);
            elementHost1.Child = _editor;
        }
    }
}
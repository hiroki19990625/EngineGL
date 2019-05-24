using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using EngineGL.Editor.Core.Window;
using RoslynPad.Editor;
using RoslynPad.Roslyn;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Controls.Window
{
    public partial class CodeEditorWindow : DockContent, IWindow
    {
        private RoslynCodeEditor _editor;
        private RoslynHost _host;

        public CodeEditorWindow()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            DockAreas = DockAreas.Document | DockAreas.Float;
        }

        public Guid WindowId { get; } = Guid.NewGuid();

        public void WindowToolStrip(ToolStrip toolStrip)
        {
            throw new NotImplementedException();
        }

        private void CodeEditorWindow_Load(object sender, EventArgs e)
        {
            _host = new RoslynHost(additionalAssemblies: new[]
            {
                Assembly.Load("RoslynPad.Roslyn.Windows"),
                Assembly.Load("RoslynPad.Editor.Windows")
            });
            _editor = new RoslynCodeEditor();
            _editor.Initialize(_host, new ClassificationHighlightColors(), Directory.GetCurrentDirectory(),
                string.Empty);
            elementHost1.Child = _editor;
        }
    }
}
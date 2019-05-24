using System;
using System.Windows.Forms;
using EngineGL.Editor.Core.Window;
using EngineGL.Editor.Event;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Controls.Window
{
    public partial class GameWindow : DockContent, IDocumentWindow
    {
        public event EventHandler<GLControlEventArgs> GLLoad;
        public event EventHandler<GLControlEventArgs> GLRender;
        public event EventHandler<GLControlEventArgs> GLResize;

        public Guid WindowId { get; }

        public void WindowToolStrip(ToolStrip toolStrip)
        {
            throw new NotImplementedException();
        }

        public GameWindow()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            DockAreas = DockAreas.Document | DockAreas.Float;
        }

        private void application_Idle(object sender, EventArgs e)
        {
            if (glControl.IsIdle)
                GLRender?.Invoke(this, new GLControlEventArgs(glControl));
        }

        private void gameWindow_Closed(object sender, EventArgs e)
        {
            Application.Idle -= application_Idle;
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            GLLoad?.Invoke(this, new GLControlEventArgs(glControl));

            Application.Idle += application_Idle;
        }

        private void glControl_Resize(object sender, EventArgs e)
        {
            GLResize?.Invoke(this, new GLControlEventArgs(glControl));
        }
    }
}
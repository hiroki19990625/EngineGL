using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EngineGL.Editor.Controls;
using OpenTK.Input;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Window
{
    public partial class Main : Form
    {
        public FormEditor Editor { get; }

        public Main()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;

            Editor = new FormEditor();


            OpenInspectorToolWindow();
            OpenNodeEditorWindow();
            OpenToolBoxWindow();
            OpenGameWindow();
            OpenCodeEditorWindow();
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        public void OpenInspectorToolWindow()
        {
            InspectorToolWindow toolWindow = new InspectorToolWindow();
            toolWindow.Show(dockPanel1, DockState.DockRight);
        }

        public void OpenNodeEditorWindow()
        {
            NodeEditorWindow nodeEditorWindow = new NodeEditorWindow();
            nodeEditorWindow.Show(dockPanel1, DockState.Document);
        }

        public void OpenToolBoxWindow()
        {
            ToolBoxWindow toolBoxWindow = new ToolBoxWindow();
            toolBoxWindow.Show(dockPanel1, DockState.DockBottom);
        }

        public void OpenGameWindow()
        {
            GameWindow gameWindow = new GameWindow();
            gameWindow.Show(dockPanel1, DockState.Document);
            gameWindow.GLLoad += GameWindow_GLLoad;
            gameWindow.GLRender += GameWindow_GLRender;
            gameWindow.GLResize += GameWindow_GLResize;
        }

        public void OpenCodeEditorWindow()
        {
            CodeEditorWindow codeEditorWindow = new CodeEditorWindow();
            codeEditorWindow.Show(dockPanel1, DockState.Document);
        }

        private void GameWindow_GLLoad(object sender, Event.GLControlEventArgs e)
        {
            Editor.Load(e.GLControl.Handle);
        }

        private void GameWindow_GLRender(object sender, Event.GLControlEventArgs e)
        {
            MouseState cursorState = Mouse.GetCursorState();
            Editor.Render(Focused, e.GLControl.PointToClient(new Point(cursorState.X, cursorState.Y)),
                e.GLControl.ClientRectangle.Size);
            e.GLControl.SwapBuffers();
        }

        private void GameWindow_GLResize(object sender, Event.GLControlEventArgs e)
        {
            Editor.Resize(e.GLControl.ClientRectangle);
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;
using EngineGL.Editor.Controls.Window;
using EngineGL.Editor.Core.Window;
using EngineGL.Editor.Event;
using OpenTK.Input;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Window
{
    public partial class MainWindow : Form, IMainWindow
    {
        public EditorInstance Instance { get; }

        public MenuStrip MenuStrip => menuStrip;
        public ToolStrip ToolStrip => toolStrip;
        public StatusStrip StatusStrip => statusStrip;

        public Guid WindowId { get; } = Guid.NewGuid();

        public void WindowToolStrip(ToolStrip toolStrip)
        {
            this.toolStrip.Items.Add("test");
        }

        public MainWindow()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;

            Instance = new EditorInstance(this);

            OpenInspectorToolWindow();
            OpenNodeEditorWindow();
            OpenToolBoxWindow();
            OpenGameWindow();
            OpenCodeEditorWindow();
        }

        public IWindow OpenCodeEditorWindow()
        {
            CodeEditorWindow codeEditorWindow = new CodeEditorWindow();
            codeEditorWindow.Show(dockPanel1, DockState.Document);

            return codeEditorWindow;
        }

        public IWindow OpenGameEditorWindow()
        {
            return null;
        }

        public IWindow OpenGameObjectListWindow()
        {
            return null;
        }

        public IWindow OpenGameWindow()
        {
            GameWindow gameWindow = new GameWindow();
            gameWindow.Show(dockPanel1, DockState.Document);
            gameWindow.GLLoad += GameWindow_GLLoad;
            gameWindow.GLRender += GameWindow_GLRender;
            gameWindow.GLResize += GameWindow_GLResize;

            return gameWindow;
        }

        public IWindow OpenInspectorToolWindow()
        {
            InspectorToolWindow toolWindow = new InspectorToolWindow();
            toolWindow.Show(dockPanel1, DockState.DockRight);

            return toolWindow;
        }

        public IWindow OpenNodeEditorWindow()
        {
            NodeEditorWindow nodeEditorWindow = new NodeEditorWindow();
            nodeEditorWindow.Show(dockPanel1, DockState.Document);

            return nodeEditorWindow;
        }

        public IWindow OpenProjectWindow()
        {
            return null;
        }

        public IWindow OpenToolBoxWindow()
        {
            ToolBoxWindow toolBoxWindow = new ToolBoxWindow();
            toolBoxWindow.Show(dockPanel1, DockState.DockBottom);

            return toolBoxWindow;
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void GameWindow_GLLoad(object sender, GLControlEventArgs e)
        {
            Instance.Handler.Load(e.GLControl.Handle);
        }

        private void GameWindow_GLRender(object sender, GLControlEventArgs e)
        {
            MouseState cursorState = Mouse.GetCursorState();
            Instance.Handler.Render(Focused, e.GLControl.PointToClient(new Point(cursorState.X, cursorState.Y)),
                e.GLControl.ClientRectangle.Size);
            e.GLControl.SwapBuffers();
        }

        private void GameWindow_GLResize(object sender, GLControlEventArgs e)
        {
            Instance.Handler.Resize(e.GLControl.ClientRectangle);
        }

        private void gameSceneFileGToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;
using EngineGL.Editor.Controls.Window;
using EngineGL.Editor.Core.Window;
using EngineGL.Editor.Event;
using EngineGL.Editor.Impl;
using EngineGL.Impl;
using OpenTK.Input;
using WeifenLuo.WinFormsUI.Docking;

namespace EngineGL.Editor.Window
{
    public partial class MainWindow : Form, IMainWindow
    {
        public EditorInstance Instance { get; }

        public GameWindowHandler Handler { get; } = new GameWindowHandler();

        public MenuStrip MenuStrip => menuStrip;
        public ToolStrip ToolStrip => toolStrip;
        public StatusStrip StatusStrip => statusStrip;

        public Guid WindowId { get; } = Guid.NewGuid();

        public MainWindow()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;

            Instance = new EditorInstance(this);

            OpenInspectorToolWindow();
            OpenNodeEditorWindow();
            OpenToolBoxWindow();
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

        public IWindow OpenGameWindow(string fileName)
        {
            GameWindow gameWindow = new GameWindow(Instance);
            gameWindow.GLLoad += GameWindow_GLLoad;
            gameWindow.GLRender += GameWindow_GLRender;
            gameWindow.GLResize += GameWindow_GLResize;
            gameWindow.Show(dockPanel1, DockState.Document);

            int hash = Handler.Game.PreLoadScene<Scene>(fileName).Value;
            Handler.Game.LoadScene(hash);

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
            Handler.Load(e.GLControl.Handle);
        }

        private void GameWindow_GLRender(object sender, GLControlEventArgs e)
        {
            MouseState cursorState = Mouse.GetCursorState();
            Handler.Render(Focused, e.GLControl.PointToClient(new Point(cursorState.X, cursorState.Y)),
                e.GLControl.ClientRectangle.Size);
            e.GLControl.SwapBuffers();
        }

        private void GameWindow_GLResize(object sender, GLControlEventArgs e)
        {
            Handler.Resize(e.GLControl.ClientRectangle);
        }

        private void solutionFileSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Solution File|*.sln";

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
                OpenProjectWindow();
        }
    }
}
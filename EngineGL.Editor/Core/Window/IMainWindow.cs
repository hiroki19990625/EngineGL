using System.Windows.Forms;

namespace EngineGL.Editor.Core.Window
{
    public interface IMainWindow : IWindow
    {
        EditorInstance Instance { get; }

        MenuStrip MenuStrip { get; }
        ToolStrip ToolStrip { get; }
        StatusStrip StatusStrip { get; }

        IWindow OpenCodeEditorWindow();
        IWindow OpenGameEditorWindow();
        IWindow OpenGameObjectListWindow();
        IWindow OpenGameWindow(string fileName);
        IWindow OpenInspectorToolWindow();
        IWindow OpenNodeEditorWindow();
        IWindow OpenProjectWindow();
        IWindow OpenToolBoxWindow();
    }
}
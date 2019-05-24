using System.Windows.Forms;

namespace EngineGL.Editor.Core.Window
{
    public interface IMainWindow
    {
        EditorInstance Instance { get; }


        MenuStrip MenuStrip { get; }
        ToolStrip ToolStrip { get; }
        StatusStrip StatusStrip { get; }
    }
}
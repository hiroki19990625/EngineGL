using EngineGL.Editor.Core.Window;
using EngineGL.Editor.Impl;

namespace EngineGL.Editor
{
    public class EditorInstance
    {
        public IMainWindow MainWindow { get; }

        public EditorInstance(IMainWindow mainWindow)
        {
            MainWindow = mainWindow;
        }
    }
}
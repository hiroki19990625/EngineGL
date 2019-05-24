using System;
using System.Collections.Concurrent;
using EngineGL.Editor.Core.Window;
using EngineGL.Editor.Impl;

namespace EngineGL.Editor
{
    public class EditorInstance
    {
        private IMainWindow _mainWindow;
        public GameWindowHandler Handler { get; } = new GameWindowHandler();

        public EditorInstance(IMainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }
    }
}
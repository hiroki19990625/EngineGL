using System.IO;
using EngineGL.Core;

namespace EngineGL.Event.Game
{
    public class PreLoadSceneEventArgs : GameEventArgs
    {
        public FileInfo SceneFile { get; }
        public IScene PreLoadScene { get; }

        public PreLoadSceneEventArgs(IGame game, FileInfo file, IScene preLoadScene) : base(game)
        {
            SceneFile = file;
            PreLoadScene = preLoadScene;
        }
    }
}
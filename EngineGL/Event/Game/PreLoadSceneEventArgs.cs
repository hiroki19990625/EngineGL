using System.IO;
using EngineGL.Core;

namespace EngineGL.Event.Game
{
    public class PreLoadSceneEventArgs : GameEventArgs, CancelableEvent
    {
        public bool IsCanceled { get; set; }
        public FileInfo SceneFile { get; set; }
        public IScene PreLoadScene { get; set; }

        public PreLoadSceneEventArgs(IGame game, FileInfo file, IScene preLoadScene) : base(game)
        {
            SceneFile = file;
            PreLoadScene = preLoadScene;
        }
    }
}
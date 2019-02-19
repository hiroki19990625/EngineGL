using EngineGL.Core;

namespace EngineGL.Event.Game
{
    public class PreUnloadSceneEventArgs : GameEventArgs, CancelableEvent
    {
        public bool IsCanceled { get; set; }
        public IScene PreUnloadScene { get; set; }

        public PreUnloadSceneEventArgs(IGame game, IScene preLoadScene) : base(game)
        {
            PreUnloadScene = preLoadScene;
        }
    }
}
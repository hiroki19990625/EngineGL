using EngineGL.Core;

namespace EngineGL.Event.Game
{
    public class UnloadSceneEventArgs : GameEventArgs, CancelableEvent
    {
        public bool IsCanceled { get; set; }
        public IScene UnloadScene { get; set; }

        public UnloadSceneEventArgs(IGame game, IScene scene) : base(game)
        {
            UnloadScene = scene;
        }
    }
}
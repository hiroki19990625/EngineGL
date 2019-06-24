using EngineGL.Core;

namespace EngineGL.Event.Game
{
    public class UnloadSceneEventArgs : GameEventArgs
    {
        public IScene UnloadScene { get; }

        public UnloadSceneEventArgs(IGame game, IScene scene) : base(game)
        {
            UnloadScene = scene;
        }
    }
}
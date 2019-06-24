using EngineGL.Core;

namespace EngineGL.Event.Game
{
    public class PreUnloadSceneEventArgs : GameEventArgs
    {
        public IScene PreUnloadScene { get; }

        public PreUnloadSceneEventArgs(IGame game, IScene preLoadScene) : base(game)
        {
            PreUnloadScene = preLoadScene;
        }
    }
}
using EngineGL.Core;

namespace EngineGL.Event.Game
{
    public class LoadSceneEventArgs : GameEventArgs
    {
        public IScene LoadScene { get; }

        public LoadSceneEventArgs(IGame game, IScene scene) : base(game)
        {
            LoadScene = scene;
        }
    }
}
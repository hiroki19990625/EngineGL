using EngineGL.Core;

namespace EngineGL.Event.Game
{
    public class LoadSceneEventArgs : GameEventArgs, CancelableEvent
    {
        public bool IsCanceled { get; set; }
        public IScene LoadScene { get; set; }

        public LoadSceneEventArgs(IGame game, IScene scene) : base(game)
        {
            LoadScene = scene;
        }
    }
}
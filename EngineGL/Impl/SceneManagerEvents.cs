using EngineGL.Event.Game;
using System;

namespace EngineGL.Impl
{
    public interface ISceneManagerEvents
    {
        event EventHandler<LoadSceneEventArgs> LoadSceneEvent;
        event EventHandler<UnloadSceneEventArgs> UnloadSceneEvent;
        event EventHandler<PreLoadSceneEventArgs> PreLoadSceneEvent;
        event EventHandler<PreUnloadSceneEventArgs> PreUnloadSceneEvent;
    }

    class SceneManagerEvents : ISceneManagerEvents
    {
        public EventHandler<LoadSceneEventArgs> LoadSceneDelegate => LoadSceneEvent;
        public EventHandler<UnloadSceneEventArgs> UnloadSceneDelegate => UnloadSceneEvent;
        public EventHandler<PreLoadSceneEventArgs> PreLoadSceneDelegate => PreLoadSceneEvent;
        public EventHandler<PreUnloadSceneEventArgs> PreUnloadSceneDelegate => PreUnloadSceneEvent;
        public event EventHandler<LoadSceneEventArgs> LoadSceneEvent;
        public event EventHandler<UnloadSceneEventArgs> UnloadSceneEvent;
        public event EventHandler<PreLoadSceneEventArgs> PreLoadSceneEvent;
        public event EventHandler<PreUnloadSceneEventArgs> PreUnloadSceneEvent;
    }
}

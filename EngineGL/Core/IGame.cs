using System;
using System.Collections.Concurrent;
using System.IO;
using EngineGL.Core.LifeCycle;
using EngineGL.Core.Utils;
using EngineGL.Event.Game;

namespace EngineGL.Core
{
    public interface IGame : Initialzeable, IDestroyable, INameable
    {
        ConcurrentDictionary<int, IScene> PreLoadedScenes { get; }
        ConcurrentDictionary<int, IScene> LoadedScenes { get; }

        event EventHandler<LoadSceneEventArgs> LoadSceneEvent;
        event EventHandler<UnloadSceneEventArgs> UnloadSceneEvent;
        event EventHandler<PreLoadSceneEventArgs> PreLoadSceneEvent;
        event EventHandler<PreUnloadSceneEventArgs> PreUnloadSceneEvent;

        Result<int> PreLoadScene(string file);
        Result<int> PreLoadScene(FileInfo file);

        void PreUnloadScene(int hash);
        void PreUnloadScenes();

        Result<IScene> GetScene(int hash);
        Result<T> GetSceneUnsafe<T>(int hash) where T : IScene;

        Result<IScene> LoadScene(int hash);
        Result<IScene> LoadScene(IScene scene);
        Result<T> LoadSceneUnsafe<T>(int hash) where T : IScene;
        Result<T> LoadSceneUnsafe<T>(T scene) where T : IScene;

        Result<IScene> UnloadScene(int hash);
        Result<IScene> UnloadScene(IScene scene);
        Result<T> UnloadSceneUnsafe<T>(int hash) where T : IScene;
        Result<T> UnloadSceneUnsafe<T>(T scene) where T : IScene;

        Result<IScene> LoadNextScene(int hash);
        Result<IScene> LoadNextScene(IScene scene);
        Result<T> LoadNextSceneUnsafe<T>(int hash) where T : IScene;
        Result<T> LoadNextSceneUnsafe<T>(T scene) where T : IScene;
    }
}
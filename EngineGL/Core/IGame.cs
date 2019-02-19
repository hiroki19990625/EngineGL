using System.Collections.Concurrent;
using System.IO;
using EngineGL.Core.Utils;

namespace EngineGL.Core
{
    public interface IGame
    {
        ConcurrentDictionary<int, IScene> PreLoadedScenes { get; }
        ConcurrentDictionary<int, IScene> LoadedScenes { get; }

        Result<int> PreLoadScene(string file);
        Result<int> PreLoadScene(FileInfo file);

        void PreUnloadScene(int hash);
        void PreUnloadScenes();

        Result<IScene> LoadScene(int hash);
        Result<IScene> LoadScene(IScene scene);
        Result<T> LoadSceneUnsafe<T>(int hash) where T : IScene;
        Result<T> LoadSceneUnsafe<T>(T scene) where T : IScene;

        Result<IScene> UnloadScene(int hash);
        Result<IScene> UnloadScene(IScene scene);
        Result<T> UnloadSceneUnsafe<T>(int hash) where T : IScene;
        Result<T> UnloadSceneUnsafe<T>(T scene) where T : IScene;
    }
}
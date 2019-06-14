using System.IO;
using EngineGL.Core.LifeCycle;
using EngineGL.Impl;
using EngineGL.Utils;

namespace EngineGL.Core
{
    public interface IGame : Initialzeable, IDestroyable, INameable
    {
        ISceneManagerEvents SceneEvents { get; }

        Result<int> PreLoadScene<T>(string file) where T : IScene;
        Result<int> PreLoadScene<T>(FileInfo file) where T : IScene;

        bool PreUnloadScene(int hash);
        bool PreUnloadScenes();

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
        bool UnloadScenes();

        Result<IScene> LoadNextScene(int hash);
        Result<IScene> LoadNextScene(IScene scene);
        Result<T> LoadNextSceneUnsafe<T>(int hash) where T : IScene;
        Result<T> LoadNextSceneUnsafe<T>(T scene) where T : IScene;

        /// <summary>
        /// フレームレートを指定して世界を開始する
        /// </summary>
        /// <param name="updateRate">設定するフレームレート</param>
         void Run(double updateRate);

        /// <summary>
        /// Maxフレームレートで世界を開始する
        /// </summary>
        void Run();
    }
}
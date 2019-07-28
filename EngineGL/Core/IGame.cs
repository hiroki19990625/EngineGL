using System;
using System.IO;
using EngineGL.Core.LifeCycle;
using EngineGL.Impl;
using EngineGL.Utils;

namespace EngineGL.Core
{
    public interface IGame : Initialzeable, IDestroyable, INameable
    {
        ISceneManagerEvents SceneEvents { get; }

        Result<Guid> PreLoadScene<T>(string file) where T : IScene;
        Result<Guid> PreLoadScene<T>(FileInfo file) where T : IScene;

        bool PreUnloadScene(Guid hash);
        bool PreUnloadScenes();

        Result<IScene> GetScene(Guid hash);
        Result<T> GetSceneUnsafe<T>(Guid hash) where T : IScene;

        Result<IScene> LoadScene(Guid hash);
        Result<IScene> LoadScene(IScene scene);
        Result<T> LoadSceneUnsafe<T>(Guid hash) where T : IScene;
        Result<T> LoadSceneUnsafe<T>(T scene) where T : IScene;

        Result<IScene> UnloadScene(Guid hash);
        Result<IScene> UnloadScene(IScene scene);
        Result<T> UnloadSceneUnsafe<T>(Guid hash) where T : IScene;
        Result<T> UnloadSceneUnsafe<T>(T scene) where T : IScene;
        bool UnloadScenes();

        Result<IScene> LoadNextScene(Guid hash);
        Result<IScene> LoadNextScene(IScene scene);
        Result<T> LoadNextSceneUnsafe<T>(Guid hash) where T : IScene;
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
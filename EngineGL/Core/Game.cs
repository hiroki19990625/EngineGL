using System;
using System.Collections.Concurrent;
using System.IO;
using EngineGL.Core.Utils;
using EngineGL.Event.Game;
using EngineGL.Event.LifeCycle;
using OpenTK;

namespace EngineGL.Core
{
    public class Game : GameWindow, IGame
    {
        public string Name { get; set; }
        public ConcurrentDictionary<int, IScene> PreLoadedScenes { get; }
        public ConcurrentDictionary<int, IScene> LoadedScenes { get; }
        public event EventHandler<InitialzeEventArgs> Initialze;
        public event EventHandler<DestroyEventArgs> Destroy;
        public event EventHandler<LoadSceneEventArgs> LoadSceneEvent;
        public event EventHandler<UnloadSceneEventArgs> UnloadSceneEvent;
        public event EventHandler<PreLoadSceneEventArgs> PreLoadSceneEvent;
        public event EventHandler<PreUnloadSceneEventArgs> PreUnloadSceneEvent;

        public void OnInitialze()
        {
            Initialze?.Invoke(this, new InitialzeEventArgs(this));
        }

        public void OnDestroy()
        {
            Destroy?.Invoke(this, new DestroyEventArgs(this));
        }

        public Result<int> PreLoadScene(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            return PreLoadScene(fileInfo);
        }

        public Result<int> PreLoadScene(FileInfo file)
        {
            IScene scene = new Scene(file);
            int hash = scene.GetHashCode();
            EventManager
                .Call(PreLoadSceneEvent,
                    this,
                    new PreLoadSceneEventArgs(this, file, scene),
                    ev => { PreLoadedScenes.TryAdd(hash, scene); },
                    ev => {}
                );
            
            return Result<int>.Success(hash);
        }

        public bool PreUnloadScene(int hash)
        {
            if (PreLoadedScenes.TryGetValue(hash, out IScene scene))
            {
                bool result = false;
                EventManager
                    .Call(PreUnloadSceneEvent,
                        this,
                        new PreUnloadSceneEventArgs(this, scene),
                        ev =>
                        {
                            PreLoadedScenes.TryRemove(hash, out scene);
                            result = true;
                        },
                        ev => {}
                    );

                return result;
            }

            return false;
        }

        public bool PreUnloadScenes()
        {
            int c = 0;
            foreach (int hash in PreLoadedScenes.Keys)
            {
                if (PreUnloadScene(hash))
                    c++;
            }

            return c > 0;
        }

        public Result<IScene> GetScene(int hash)
        {
            throw new NotImplementedException();
        }

        public Result<T> GetSceneUnsafe<T>(int hash) where T : IScene
        {
            throw new NotImplementedException();
        }

        public Result<IScene> LoadScene(int hash)
        {
            throw new NotImplementedException();
        }

        public Result<IScene> LoadScene(IScene scene)
        {
            throw new NotImplementedException();
        }

        public Result<T> LoadSceneUnsafe<T>(int hash) where T : IScene
        {
            throw new NotImplementedException();
        }

        public Result<T> LoadSceneUnsafe<T>(T scene) where T : IScene
        {
            throw new NotImplementedException();
        }

        public Result<IScene> UnloadScene(int hash)
        {
            throw new NotImplementedException();
        }

        public Result<IScene> UnloadScene(IScene scene)
        {
            throw new NotImplementedException();
        }

        public Result<T> UnloadSceneUnsafe<T>(int hash) where T : IScene
        {
            throw new NotImplementedException();
        }

        public Result<T> UnloadSceneUnsafe<T>(T scene) where T : IScene
        {
            throw new NotImplementedException();
        }

        public Result<IScene> LoadNextScene(int hash)
        {
            throw new NotImplementedException();
        }

        public Result<IScene> LoadNextScene(IScene scene)
        {
            throw new NotImplementedException();
        }

        public Result<T> LoadNextSceneUnsafe<T>(int hash) where T : IScene
        {
            throw new NotImplementedException();
        }

        public Result<T> LoadNextSceneUnsafe<T>(T scene) where T : IScene
        {
            throw new NotImplementedException();
        }
    }
}
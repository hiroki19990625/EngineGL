using System;
using System.Collections.Concurrent;
using System.IO;
using EngineGL.Core;
using EngineGL.Core.Utils;
using EngineGL.Event.Game;
using EngineGL.Event.LifeCycle;
using OpenTK;

namespace EngineGL.Impl
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

            PreLoadSceneEventArgs args = new PreLoadSceneEventArgs(this, file, scene);
            EventManager<PreLoadSceneEventArgs> manager
                = new EventManager<PreLoadSceneEventArgs>(PreLoadSceneEvent, this, args);
            manager.OnSuccess = ev => PreLoadedScenes.TryAdd(hash, scene);

            if (manager.Call())
                return Result<int>.Success(hash);
            else
                return Result<int>.Fail();
        }

        public bool PreUnloadScene(int hash)
        {
            if (PreLoadedScenes.TryGetValue(hash, out IScene scene))
            {
                PreUnloadSceneEventArgs args = new PreUnloadSceneEventArgs(this, scene);
                EventManager<PreUnloadSceneEventArgs> manager
                    = new EventManager<PreUnloadSceneEventArgs>(PreUnloadSceneEvent, this, args);
                manager.OnSuccess = ev => PreLoadedScenes.TryRemove(args.PreUnloadScene.GetHashCode(), out scene);
                return manager.Call();
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
            if (LoadedScenes.TryGetValue(hash, out IScene scene))
            {
                return Result<IScene>.Success(scene);
            }

            return Result<IScene>.Fail();
        }

        public Result<T> GetSceneUnsafe<T>(int hash) where T : IScene
        {
            if (LoadedScenes.TryGetValue(hash, out IScene scene))
            {
                return Result<T>.Success((T) scene);
            }

            return Result<T>.Fail();
        }

        public Result<IScene> LoadScene(int hash)
        {
            if (PreLoadedScenes.TryGetValue(hash, out IScene scene)
                || !LoadedScenes.ContainsKey(hash))
            {
                LoadSceneEventArgs args = new LoadSceneEventArgs(this, scene);
                EventManager<LoadSceneEventArgs> manager
                    = new EventManager<LoadSceneEventArgs>(LoadSceneEvent, this, args);
                manager.OnSuccess = ev => LoadedScenes.TryAdd(ev.LoadScene.GetHashCode(), ev.LoadScene);
                
                if (manager.Call())
                    return Result<IScene>.Success(scene);
                else
                    return Result<IScene>.Fail();
            }

            return Result<IScene>.Fail();
        }

        public Result<IScene> LoadScene(IScene scene)
        {
            int hash = scene.GetHashCode();
            if (!LoadedScenes.ContainsKey(hash))
            {
                LoadSceneEventArgs args = new LoadSceneEventArgs(this, scene);
                EventManager<LoadSceneEventArgs> manager
                    = new EventManager<LoadSceneEventArgs>(LoadSceneEvent, this, args);
                manager.OnSuccess = ev => 
            }
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
using EngineGL.Core;
using EngineGL.Event.Game;
using EngineGL.FormatMessage;
using EngineGL.Utils;
using NLog;
using System;
using System.Collections.Concurrent;
using System.IO;

namespace EngineGL.Impl
{
    class SceneManager
    {
        private readonly ConcurrentDictionary<int, IScene> _preLoadedScenes = new ConcurrentDictionary<int, IScene>();

        private readonly ConcurrentDictionary<int, IScene> _loadedScenes = new ConcurrentDictionary<int, IScene>();

        private readonly Logger logger;

        private readonly SceneManagerEvents events = new SceneManagerEvents();

        public ISceneManagerEvents Events => events;

        public SceneManager(Logger logger) => this.logger = logger;

        public virtual Result<int> PreLoadScene<T>(string file, IGame game) where T : IScene
        {
            FileInfo fileInfo = new FileInfo(file);
            return PreLoadScene<T>(fileInfo, game);
        }

        public virtual Result<int> PreLoadScene<T>(FileInfo file, IGame game) where T : IScene
        {
            StreamReader reader = file.OpenText();
            IScene old = reader.ReadToEnd().FromDeserializableJson<T>();
            T scene = Activator.CreateInstance<T>();
            reader.Close();
            foreach (IObject s in old.GetObjects().Value)
            {
                if (s is IComponentAttachable)
                {
                    IComponentAttachable attachable = (IComponentAttachable)s;
                    foreach (IComponent component in attachable.GetComponents().Value)
                    {
                        component.ParentObject = attachable;
                    }
                }

                scene.AddObject(s);
            }

            PreLoadSceneEventArgs args = new PreLoadSceneEventArgs(game, file, scene);
            EventManager<PreLoadSceneEventArgs> manager
                = new EventManager<PreLoadSceneEventArgs>(events.PreLoadSceneDelegate, game, args);
            manager.OnSuccess = ev => _preLoadedScenes.TryAdd(ev.PreLoadScene.GetHashCode(), ev.PreLoadScene);

            if (manager.Call())
                return Result<int>.Success(args.PreLoadScene.GetHashCode());
            else
                return Result<int>.Fail();
        }

        public virtual bool PreUnloadScene(int hash, IGame game)
        {
            if (_preLoadedScenes.TryGetValue(hash, out IScene scene))
            {
                PreUnloadSceneEventArgs args = new PreUnloadSceneEventArgs(game, scene);
                EventManager<PreUnloadSceneEventArgs> manager
                    = new EventManager<PreUnloadSceneEventArgs>(events.PreUnloadSceneDelegate, game, args);
                manager.OnSuccess = ev => _preLoadedScenes.TryRemove(ev.PreUnloadScene.GetHashCode(), out scene);
                return manager.Call();
            }

            return false;
        }

        public virtual bool PreUnloadScenes(IGame game)
        {
            int c = 0;
            foreach (int hash in _preLoadedScenes.Keys)
            {
                if (PreUnloadScene(hash, game))
                    c++;
            }

            return c > 0;
        }

        public virtual Result<IScene> GetScene(int hash)
        {
            if (_loadedScenes.TryGetValue(hash, out IScene scene))
            {
                return Result<IScene>.Success(scene);
            }

            return Result<IScene>.Fail();
        }

        public virtual Result<T> GetSceneUnsafe<T>(int hash) where T : IScene
        {
            if (_loadedScenes.TryGetValue(hash, out IScene scene))
            {
                return Result<T>.Success((T)scene);
            }

            return Result<T>.Fail();
        }

        public virtual Result<IScene> LoadScene(int hash, IGame game)
        {
            if (_preLoadedScenes.TryGetValue(hash, out IScene scene)
                || !_loadedScenes.ContainsKey(hash))
            {
                LoadSceneEventArgs args = new LoadSceneEventArgs(game, scene);
                EventManager<LoadSceneEventArgs> manager
                    = new EventManager<LoadSceneEventArgs>(events.LoadSceneDelegate, game, args);
                manager.OnSuccess = ev => _loadedScenes.TryAdd(ev.LoadScene.GetHashCode(), ev.LoadScene);

                if (manager.Call())
                    return Result<IScene>.Success(args.LoadScene);
                else
                    return Result<IScene>.Fail();
            }

            return Result<IScene>.Fail();
        }

        public virtual Result<IScene> LoadScene(IScene scene, IGame game)
        {
            int hash = scene.GetHashCode();
            if (!_loadedScenes.ContainsKey(hash))
            {
                LoadSceneEventArgs args = new LoadSceneEventArgs(game, scene);
                EventManager<LoadSceneEventArgs> manager
                    = new EventManager<LoadSceneEventArgs>(events.LoadSceneDelegate, game, args);
                manager.OnSuccess = ev => _loadedScenes.TryAdd(ev.LoadScene.GetHashCode(), ev.LoadScene);

                if (manager.Call())
                    return Result<IScene>.Success(args.LoadScene);
                else
                    return Result<IScene>.Fail();
            }

            return Result<IScene>.Fail();
        }

        public virtual Result<T> LoadSceneUnsafe<T>(int hash, IGame game) where T : IScene
        {
            try
            {
                return Result<T>.Success((T)LoadScene(hash, game).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                logger.Debug(e);
                return Result<T>.Fail(e.ToString());
            }
        }

        public virtual Result<T> LoadSceneUnsafe<T>(T scene, IGame game) where T : IScene
        {
            try
            {
                return Result<T>.Success((T)LoadScene(scene, game).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                logger.Debug(e);
                return Result<T>.Fail(e.ToString());
            }
        }

        public virtual Result<IScene> UnloadScene(int hash, IGame game)
        {
            if (_loadedScenes.TryGetValue(hash, out IScene scene))
            {
                UnloadSceneEventArgs args = new UnloadSceneEventArgs(game, scene);
                EventManager<UnloadSceneEventArgs> manager
                    = new EventManager<UnloadSceneEventArgs>(events.UnloadSceneDelegate, game, args);
                manager.OnSuccess = ev => _loadedScenes.TryRemove(args.UnloadScene.GetHashCode(), out scene);

                if (manager.Call())
                    return Result<IScene>.Success(args.UnloadScene);
                else
                    return Result<IScene>.Fail();
            }

            return Result<IScene>.Fail();
        }

        public virtual Result<IScene> UnloadScene(IScene scene, IGame game)
        {
            return UnloadScene(scene.GetHashCode(), game);
        }

        public virtual Result<T> UnloadSceneUnsafe<T>(int hash, IGame game) where T : IScene
        {
            try
            {
                return Result<T>.Success((T)UnloadScene(hash, game).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                logger.Debug(e);
                return Result<T>.Fail(e.ToString());
            }
        }

        public virtual Result<T> UnloadSceneUnsafe<T>(T scene, IGame game) where T : IScene
        {
            try
            {
                return Result<T>.Success((T)UnloadScene(scene, game).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                logger.Debug(e);
                return Result<T>.Fail(e.ToString());
            }
        }

        public virtual bool UnloadScenes(IGame game)
        {
            int c = 0;
            foreach (int hash in _loadedScenes.Keys)
            {
                if (UnloadScene(hash, game).IsSuccess)
                    c++;
            }

            return c > 0;
        }

        public virtual Result<IScene> LoadNextScene(int hash, IGame game)
        {
            UnloadScenes(game);
            return LoadScene(hash, game);
        }

        public virtual Result<IScene> LoadNextScene(IScene scene, IGame game)
        {
            UnloadScenes(game);
            return LoadScene(scene, game);
        }

        public virtual Result<T> LoadNextSceneUnsafe<T>(int hash, IGame game) where T : IScene
        {
            UnloadScenes(game);
            try
            {
                return Result<T>.Success((T)LoadScene(hash, game).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                logger.Debug(e);
                return Result<T>.Fail(e.ToString());
            }
        }

        public virtual Result<T> LoadNextSceneUnsafe<T>(T scene, IGame game) where T : IScene
        {
            UnloadScenes(game);
            try
            {
                return Result<T>.Success((T)LoadScene(scene, game).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                logger.Debug(e);
                return Result<T>.Fail(e.ToString());
            }
        }

        public void OnGUI(double time)
        {
            foreach (IScene scene in _loadedScenes.Values)
            {
                scene.OnGUI(time);
            }
        }

        public void OnUpdateFrame(double time)
        {
            foreach (IScene scene in _loadedScenes.Values)
            {
                scene.OnUpdate(time);
            }
        }

        public void OnDraw(double time)
        {
            foreach (IScene scene in _loadedScenes.Values)
            {
                scene.OnDraw(time);
            }
        }
    }
}

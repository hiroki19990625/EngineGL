using System;
using System.Collections.Concurrent;
using System.IO;
using EngineGL.Core;
using EngineGL.Event.Game;
using EngineGL.Utils;
using Newtonsoft.Json.Linq;
using NLog;

namespace EngineGL.Impl
{
    class SceneManager
    {
        private readonly ConcurrentDictionary<Guid, IScene> _preLoadedScenes = new ConcurrentDictionary<Guid, IScene>();

        private readonly ConcurrentDictionary<Guid, IScene> _loadedScenes = new ConcurrentDictionary<Guid, IScene>();

        private readonly Logger logger;

        private readonly SceneManagerEvents events = new SceneManagerEvents();

        public ISceneManagerEvents Events => events;

        public SceneManager(Logger logger) => this.logger = logger;

        public virtual Result<Guid> PreLoadScene<T>(string file, IGame game) where T : IScene
        {
            FileInfo fileInfo = new FileInfo(file);
            return PreLoadScene<T>(fileInfo, game);
        }

        public virtual Result<Guid> PreLoadScene<T>(FileInfo file, IGame game) where T : IScene
        {
            StreamReader reader = file.OpenText();
            string json = reader.ReadToEnd();
            T scene = Activator.CreateInstance<T>();
            reader.Close();
            scene.OnDeserializeJson(JObject.Parse(json));

            PreLoadSceneEventArgs args = new PreLoadSceneEventArgs(game, file, scene);
            EventManager<PreLoadSceneEventArgs> manager
                = new EventManager<PreLoadSceneEventArgs>(events.PreLoadSceneDelegate, game, args);
            _preLoadedScenes.TryAdd(scene.InstanceGuid, scene);

            manager.Call();
            return Result<Guid>.Success(args.PreLoadScene.InstanceGuid);
        }

        public virtual bool PreUnloadScene(Guid hash, IGame game)
        {
            if (_preLoadedScenes.TryGetValue(hash, out IScene scene))
            {
                PreUnloadSceneEventArgs args = new PreUnloadSceneEventArgs(game, scene);
                EventManager<PreUnloadSceneEventArgs> manager
                    = new EventManager<PreUnloadSceneEventArgs>(events.PreUnloadSceneDelegate, game, args);
                _preLoadedScenes.TryRemove(scene.InstanceGuid, out scene);
                manager.Call();

                return true;
            }

            return false;
        }

        public virtual bool PreUnloadScenes(IGame game)
        {
            int c = 0;
            foreach (Guid hash in _preLoadedScenes.Keys)
            {
                if (PreUnloadScene(hash, game))
                    c++;
            }

            return c > 0;
        }

        public virtual Result<IScene> GetScene(Guid hash)
        {
            if (_loadedScenes.TryGetValue(hash, out IScene scene))
            {
                return Result<IScene>.Success(scene);
            }

            return Result<IScene>.Fail();
        }

        public virtual Result<T> GetSceneUnsafe<T>(Guid hash) where T : IScene
        {
            if (_loadedScenes.TryGetValue(hash, out IScene scene))
            {
                return Result<T>.Success((T) scene);
            }

            return Result<T>.Fail();
        }

        public virtual Result<IScene> LoadScene(Guid hash, IGame game)
        {
            if (_preLoadedScenes.TryGetValue(hash, out IScene scene)
                || !_loadedScenes.ContainsKey(hash))
            {
                LoadSceneEventArgs args = new LoadSceneEventArgs(game, scene);
                EventManager<LoadSceneEventArgs> manager
                    = new EventManager<LoadSceneEventArgs>(events.LoadSceneDelegate, game, args);
                _loadedScenes.TryAdd(scene.InstanceGuid, scene);

                manager.Call();
                return Result<IScene>.Success(args.LoadScene);
            }

            return Result<IScene>.Fail();
        }

        public virtual Result<IScene> LoadScene(IScene scene, IGame game)
        {
            Guid hash = scene.InstanceGuid;
            if (!_loadedScenes.ContainsKey(hash))
            {
                LoadSceneEventArgs args = new LoadSceneEventArgs(game, scene);
                EventManager<LoadSceneEventArgs> manager
                    = new EventManager<LoadSceneEventArgs>(events.LoadSceneDelegate, game, args);
                _loadedScenes.TryAdd(scene.InstanceGuid, scene);

                manager.Call();
                return Result<IScene>.Success(args.LoadScene);
            }

            return Result<IScene>.Fail();
        }

        public virtual Result<T> LoadSceneUnsafe<T>(Guid hash, IGame game) where T : IScene
        {
            try
            {
                return Result<T>.Success((T) LoadScene(hash, game).Value);
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
                return Result<T>.Success((T) LoadScene(scene, game).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                logger.Debug(e);
                return Result<T>.Fail(e.ToString());
            }
        }

        public virtual Result<IScene> UnloadScene(Guid hash, IGame game)
        {
            if (_loadedScenes.TryGetValue(hash, out IScene scene))
            {
                UnloadSceneEventArgs args = new UnloadSceneEventArgs(game, scene);
                EventManager<UnloadSceneEventArgs> manager
                    = new EventManager<UnloadSceneEventArgs>(events.UnloadSceneDelegate, game, args);
                _loadedScenes.TryRemove(args.UnloadScene.InstanceGuid, out scene);
                manager.Call();
                return Result<IScene>.Success(args.UnloadScene);
            }

            return Result<IScene>.Fail();
        }

        public virtual Result<IScene> UnloadScene(IScene scene, IGame game)
        {
            return UnloadScene(scene.InstanceGuid, game);
        }

        public virtual Result<T> UnloadSceneUnsafe<T>(Guid hash, IGame game) where T : IScene
        {
            try
            {
                return Result<T>.Success((T) UnloadScene(hash, game).Value);
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
                return Result<T>.Success((T) UnloadScene(scene, game).Value);
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
            foreach (Guid hash in _loadedScenes.Keys)
            {
                if (UnloadScene(hash, game).IsSuccess)
                    c++;
            }

            return c > 0;
        }

        public virtual Result<IScene> LoadNextScene(Guid hash, IGame game)
        {
            UnloadScenes(game);
            return LoadScene(hash, game);
        }

        public virtual Result<IScene> LoadNextScene(IScene scene, IGame game)
        {
            UnloadScenes(game);
            return LoadScene(scene, game);
        }

        public virtual Result<T> LoadNextSceneUnsafe<T>(Guid hash, IGame game) where T : IScene
        {
            UnloadScenes(game);
            try
            {
                return Result<T>.Success((T) LoadScene(hash, game).Value);
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
                return Result<T>.Success((T) LoadScene(scene, game).Value);
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
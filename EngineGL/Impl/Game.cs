using System;
using System.Collections.Concurrent;
using System.IO;
using EngineGL.Core;
using EngineGL.Event.Game;
using EngineGL.Event.LifeCycle;
using EngineGL.Utils;
using OpenTK;

namespace EngineGL.Impl
{
    public class Game : GameWindow, IGame
    {
        private readonly ConcurrentDictionary<int, IScene> _preLoadedScenes =
            new ConcurrentDictionary<int, IScene>();

        private readonly ConcurrentDictionary<int, IScene> _loadedScenes =
            new ConcurrentDictionary<int, IScene>();

        public string Name { get; set; }
        public event EventHandler<InitialzeEventArgs> Initialze;
        public event EventHandler<DestroyEventArgs> Destroy;
        public event EventHandler<LoadSceneEventArgs> LoadSceneEvent;
        public event EventHandler<UnloadSceneEventArgs> UnloadSceneEvent;
        public event EventHandler<PreLoadSceneEventArgs> PreLoadSceneEvent;
        public event EventHandler<PreUnloadSceneEventArgs> PreUnloadSceneEvent;

        public virtual void OnInitialze()
        {
            Initialze?.Invoke(this, new InitialzeEventArgs(this));
        }

        public virtual void OnDestroy()
        {
            Destroy?.Invoke(this, new DestroyEventArgs(this));
        }

        public virtual Result<int> PreLoadScene(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            return PreLoadScene(fileInfo);
        }

        public virtual Result<int> PreLoadScene(FileInfo file)
        {
            IScene scene = new Scene(file);
            int hash = scene.GetHashCode();

            PreLoadSceneEventArgs args = new PreLoadSceneEventArgs(this, file, scene);
            EventManager<PreLoadSceneEventArgs> manager
                = new EventManager<PreLoadSceneEventArgs>(PreLoadSceneEvent, this, args);
            manager.OnSuccess = ev => _preLoadedScenes.TryAdd(ev.PreLoadScene.GetHashCode(), ev.PreLoadScene);

            if (manager.Call())
                return Result<int>.Success(args.PreLoadScene.GetHashCode());
            else
                return Result<int>.Fail();
        }

        public virtual bool PreUnloadScene(int hash)
        {
            if (_preLoadedScenes.TryGetValue(hash, out IScene scene))
            {
                PreUnloadSceneEventArgs args = new PreUnloadSceneEventArgs(this, scene);
                EventManager<PreUnloadSceneEventArgs> manager
                    = new EventManager<PreUnloadSceneEventArgs>(PreUnloadSceneEvent, this, args);
                manager.OnSuccess = ev => _preLoadedScenes.TryRemove(ev.PreUnloadScene.GetHashCode(), out scene);
                return manager.Call();
            }

            return false;
        }

        public virtual bool PreUnloadScenes()
        {
            int c = 0;
            foreach (int hash in _preLoadedScenes.Keys)
            {
                if (PreUnloadScene(hash))
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
                return Result<T>.Success((T) scene);
            }

            return Result<T>.Fail();
        }

        public virtual Result<IScene> LoadScene(int hash)
        {
            if (_preLoadedScenes.TryGetValue(hash, out IScene scene)
                || !_loadedScenes.ContainsKey(hash))
            {
                LoadSceneEventArgs args = new LoadSceneEventArgs(this, scene);
                EventManager<LoadSceneEventArgs> manager
                    = new EventManager<LoadSceneEventArgs>(LoadSceneEvent, this, args);
                manager.OnSuccess = ev => _loadedScenes.TryAdd(ev.LoadScene.GetHashCode(), ev.LoadScene);

                if (manager.Call())
                    return Result<IScene>.Success(args.LoadScene);
                else
                    return Result<IScene>.Fail();
            }

            return Result<IScene>.Fail();
        }

        public virtual Result<IScene> LoadScene(IScene scene)
        {
            int hash = scene.GetHashCode();
            if (!_loadedScenes.ContainsKey(hash))
            {
                LoadSceneEventArgs args = new LoadSceneEventArgs(this, scene);
                EventManager<LoadSceneEventArgs> manager
                    = new EventManager<LoadSceneEventArgs>(LoadSceneEvent, this, args);
                manager.OnSuccess = ev => _loadedScenes.TryAdd(ev.LoadScene.GetHashCode(), ev.LoadScene);

                if (manager.Call())
                    return Result<IScene>.Success(args.LoadScene);
                else
                    return Result<IScene>.Fail();
            }

            return Result<IScene>.Fail();
        }

        public virtual Result<T> LoadSceneUnsafe<T>(int hash) where T : IScene
        {
            try
            {
                return Result<T>.Success((T) LoadScene(hash).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                return Result<T>.Fail(e.ToString());
            }
        }

        public virtual Result<T> LoadSceneUnsafe<T>(T scene) where T : IScene
        {
            try
            {
                return Result<T>.Success((T) LoadScene(scene).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                return Result<T>.Fail(e.ToString());
            }
        }

        public virtual Result<IScene> UnloadScene(int hash)
        {
            if (_loadedScenes.TryGetValue(hash, out IScene scene))
            {
                UnloadSceneEventArgs args = new UnloadSceneEventArgs(this, scene);
                EventManager<UnloadSceneEventArgs> manager
                    = new EventManager<UnloadSceneEventArgs>(UnloadSceneEvent, this, args);
                manager.OnSuccess = ev => _loadedScenes.TryRemove(args.UnloadScene.GetHashCode(), out scene);

                if (manager.Call())
                    return Result<IScene>.Success(args.UnloadScene);
                else
                    return Result<IScene>.Fail();
            }

            return Result<IScene>.Fail();
        }

        public virtual Result<IScene> UnloadScene(IScene scene)
        {
            return UnloadScene(scene.GetHashCode());
        }

        public virtual Result<T> UnloadSceneUnsafe<T>(int hash) where T : IScene
        {
            try
            {
                return Result<T>.Success((T) UnloadScene(hash).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                return Result<T>.Fail(e.ToString());
            }
        }

        public virtual Result<T> UnloadSceneUnsafe<T>(T scene) where T : IScene
        {
            try
            {
                return Result<T>.Success((T) UnloadScene(scene).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                return Result<T>.Fail(e.ToString());
            }
        }

        public virtual bool UnloadScenes()
        {
            int c = 0;
            foreach (int hash in _loadedScenes.Keys)
            {
                if (UnloadScene(hash).IsSuccess)
                    c++;
            }

            return c > 0;
        }

        public virtual Result<IScene> LoadNextScene(int hash)
        {
            UnloadScenes();
            return LoadScene(hash);
        }

        public virtual Result<IScene> LoadNextScene(IScene scene)
        {
            UnloadScenes();
            return LoadScene(scene);
        }

        public virtual Result<T> LoadNextSceneUnsafe<T>(int hash) where T : IScene
        {
            UnloadScenes();
            try
            {
                return Result<T>.Success((T) LoadScene(hash).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                return Result<T>.Fail(e.ToString());
            }
        }

        public virtual Result<T> LoadNextSceneUnsafe<T>(T scene) where T : IScene
        {
            UnloadScenes();
            try
            {
                return Result<T>.Success((T) LoadScene(scene).Value);
            }
            catch (Exception e) when (e is InvalidCastException || e is InvalidOperationException)
            {
                return Result<T>.Fail(e.ToString());
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            try
            {
                base.OnUpdateFrame(e);

                foreach (IScene scene in _loadedScenes.Values)
                {
                    scene.OnUpdate();
                }
            }
            catch (Exception exception)
            {
                Exit(exception);
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            try
            {
                base.OnRenderFrame(e);

                foreach (IScene scene in _loadedScenes.Values)
                {
                    scene.OnDraw();
                }

                SwapBuffers();
            }
            catch (Exception exception)
            {
                Exit(exception);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            OnInitialze();

            base.OnLoad(e);
        }

        public override void Exit()
        {
            OnDestroy();
        }

        public void Exit(Exception exception)
        {
            base.Exit();
            Dialog.Open("Exception", exception.ToString(), Dialog.DialogType.ICON_ERROR);
        }

        public void Exit(string message)
        {
            base.Exit();
            Dialog.Open("Exception", message, Dialog.DialogType.ICON_ERROR);
        }
    }
}
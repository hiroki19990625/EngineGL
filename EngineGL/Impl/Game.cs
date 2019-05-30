using System;
using System.IO;
using EngineGL.Core;
using EngineGL.Event.Game;
using EngineGL.Event.LifeCycle;
using EngineGL.Utils;
using NLog;
using NLog.Config;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl
{
    public class Game : GameWindow, IGame
    {
        public static Logger Logger { get; private set; } = LogManager.GetCurrentClassLogger();


        private readonly SceneManager sceneManager = new SceneManager(Logger);

        public bool ShowExitErrorDialog { get; set; } = true;
        public bool ExceptionDialog { get; set; } = false;
        public bool ExceptionExit { get; set; } = true;

        public bool DebugLogging { get; set; } = false;
        public LoggingConfiguration LoggingConfiguration { get; set; }

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

        public virtual Result<int> PreLoadScene<T>(string file) where T : IScene
            => sceneManager.PreLoadScene<T>(file, this, PreLoadSceneEvent);

        public virtual Result<int> PreLoadScene<T>(FileInfo file) where T : IScene
            => sceneManager.PreLoadScene<T>(file, this, PreLoadSceneEvent);

        public virtual bool PreUnloadScene(int hash)
            => sceneManager.PreUnloadScene(hash, this, PreUnloadSceneEvent);

        public virtual bool PreUnloadScenes()
            => sceneManager.PreUnloadScenes(this, PreUnloadSceneEvent);

        public virtual Result<IScene> GetScene(int hash)
            => sceneManager.GetScene(hash);

        public virtual Result<T> GetSceneUnsafe<T>(int hash) where T : IScene
            => sceneManager.GetSceneUnsafe<T>(hash);

        public virtual Result<IScene> LoadScene(int hash)
            => sceneManager.LoadScene(hash, this, LoadSceneEvent);

        public virtual Result<IScene> LoadScene(IScene scene)
            => sceneManager.LoadScene(scene, this, LoadSceneEvent);

        public virtual Result<T> LoadSceneUnsafe<T>(int hash) where T : IScene
            => sceneManager.LoadSceneUnsafe<T>(hash, this, LoadSceneEvent);

        public virtual Result<T> LoadSceneUnsafe<T>(T scene) where T : IScene
            => sceneManager.LoadSceneUnsafe(scene, this, LoadSceneEvent);

        public virtual Result<IScene> UnloadScene(int hash)
            => sceneManager.UnloadScene(hash, this, UnloadSceneEvent);

        public virtual Result<IScene> UnloadScene(IScene scene)
            => sceneManager.UnloadScene(scene, this, UnloadSceneEvent);

        public virtual Result<T> UnloadSceneUnsafe<T>(int hash) where T : IScene
            => sceneManager.UnloadSceneUnsafe<T>(hash, this, UnloadSceneEvent);

        public virtual Result<T> UnloadSceneUnsafe<T>(T scene) where T : IScene
            => sceneManager.UnloadSceneUnsafe(scene, this, UnloadSceneEvent);

        public virtual bool UnloadScenes()
            => sceneManager.UnloadScenes(this, UnloadSceneEvent);

        public virtual Result<IScene> LoadNextScene(int hash)
            => sceneManager.LoadNextScene(hash, this, UnloadSceneEvent, LoadSceneEvent);

        public virtual Result<IScene> LoadNextScene(IScene scene)
            => sceneManager.LoadNextScene(scene, this, UnloadSceneEvent, LoadSceneEvent);

        public virtual Result<T> LoadNextSceneUnsafe<T>(int hash) where T : IScene
            => sceneManager.LoadNextSceneUnsafe<T>(hash, this, UnloadSceneEvent, LoadSceneEvent);

        public virtual Result<T> LoadNextSceneUnsafe<T>(T scene) where T : IScene
            => sceneManager.LoadNextSceneUnsafe<T>(scene, this, UnloadSceneEvent, LoadSceneEvent);

        public virtual void LoadDefaultFunc()
        {
            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);
        }

        public virtual void DrawDefaultFunc(FrameEventArgs ev)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public virtual void AdjustResize()
        {
            GL.Viewport(ClientRectangle);
            GL.MatrixMode(MatrixMode.Projection);
            Matrix4 projection =
                Matrix4.CreatePerspectiveFieldOfView((float) Math.PI / 4, (float) Width / (float) Height,
                    1.0f,
                    64.0f);
            GL.LoadMatrix(ref projection);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            try
            {
                base.OnUpdateFrame(e);
                sceneManager.OnUpdateFrame(e.Time);
            }
            catch (Exception exception)

            {
                Logger.Error(exception);
                Exit(exception);
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            try
            {
                base.OnRenderFrame(e);
                sceneManager.OnDraw(e.Time);
                SwapBuffers();
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
                Exit(exception);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (DebugLogging)
            {
                LogManager.Configuration = LoggingConfiguration;
            }

            OnInitialze();
            base.OnLoad(e);
        }

        public override void Exit()
        {
            OnDestroy();
        }

        public void Exit(Exception exception)
        {
            if (ExceptionExit)
            {
                Exit();
            }

            if (ShowExitErrorDialog)
            {
                /*if (ExceptionDialog)
                    Dialog.Open("Exception", exception.ToString(), Dialog.DialogType.ICON_ERROR);
                else
                    Dialog.Open("Exception", exception.Message, Dialog.DialogType.ICON_ERROR);*/
            }
        }

        public void Exit(string message)
        {
            Exit();
            //Dialog.Open("Error", message, Dialog.DialogType.ICON_ERROR);
        }
    }
}
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using EngineGL.Core;
using EngineGL.Event.Game;
using EngineGL.Event.LifeCycle;
using EngineGL.FormatMessage;
using EngineGL.Resources.Font;
using EngineGL.Structs.Math;
using EngineGL.Utils;
using ImGuiNET;
using NLog;
using NLog.Config;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace EngineGL.Impl
{
    public class Game : GameWindow, IGame
    {
        public static Logger Logger { get; private set; }

        private readonly ConcurrentDictionary<int, IScene> _preLoadedScenes =
            new ConcurrentDictionary<int, IScene>();

        private readonly ConcurrentDictionary<int, IScene> _loadedScenes =
            new ConcurrentDictionary<int, IScene>();

        private float _mouseWheel;

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
        {
            FileInfo fileInfo = new FileInfo(file);
            return PreLoadScene<T>(fileInfo);
        }

        public virtual Result<int> PreLoadScene<T>(FileInfo file) where T : IScene
        {
            StreamReader reader = file.OpenText();
            IScene old = reader.ReadToEnd().FromDeserializableJson<T>();
            T scene = Activator.CreateInstance<T>();
            reader.Close();
            foreach (IObject s in old.GetObjects().Value)
            {
                if (s is IComponentAttachable)
                {
                    IComponentAttachable attachable = (IComponentAttachable) s;
                    foreach (IComponent component in attachable.GetComponents().Value)
                    {
                        component.ParentObject = attachable;
                    }
                }

                scene.AddObject(s);
            }

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
                Logger.Debug(e);
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
                Logger.Debug(e);
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
                Logger.Debug(e);
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
                Logger.Debug(e);
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
                Logger.Debug(e);
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
                Logger.Debug(e);
                return Result<T>.Fail(e.ToString());
            }
        }

        public virtual void LoadDefaultFunc()
        {
            ImGui.SetCurrentContext(ImGui.CreateContext());

            ImGui_Init();

            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);
        }

        public virtual void DrawDefaultFunc(FrameEventArgs ev)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            ImGui_Render(ev);
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

        protected unsafe void ImGui_Init()
        {
            ImGuiIOPtr io = ImGui.GetIO();

            ImGui_Input_Init(io);

            io.ImeWindowHandle = WindowInfo.Handle;

            byte[] ttf = Fonts.meiryo;
            fixed (byte* bp = ttf)
            {
                IntPtr ptr = (IntPtr) bp;
                io.Fonts.AddFontFromMemoryTTF(ptr, 0, 20.0f, IntPtr.Zero, io.Fonts.GetGlyphRangesJapanese());
            }

            byte* tex;
            int w;
            int h;
            io.Fonts.GetTexDataAsAlpha8(out tex, out w, out h);

            int p = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, p);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) All.Linear);
            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Alpha,
                w,
                h,
                0,
                PixelFormat.Alpha,
                PixelType.UnsignedByte,
                new IntPtr(tex));

            io.Fonts.SetTexID((IntPtr) p);

            io.Fonts.ClearTexData();
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        protected unsafe void ImGui_Input_Init(ImGuiIOPtr io)
        {
            io.NativePtr->KeyMap[(int) ImGuiKey.Tab] = (int) Key.Tab;
            io.NativePtr->KeyMap[(int) ImGuiKey.LeftArrow] = (int) Key.Left;
            io.NativePtr->KeyMap[(int) ImGuiKey.RightArrow] = (int) Key.Right;
            io.NativePtr->KeyMap[(int) ImGuiKey.UpArrow] = (int) Key.Up;
            io.NativePtr->KeyMap[(int) ImGuiKey.DownArrow] = (int) Key.Down;
            io.NativePtr->KeyMap[(int) ImGuiKey.PageUp] = (int) Key.PageUp;
            io.NativePtr->KeyMap[(int) ImGuiKey.PageDown] = (int) Key.Down;
            io.NativePtr->KeyMap[(int) ImGuiKey.Home] = (int) Key.Home;
            io.NativePtr->KeyMap[(int) ImGuiKey.End] = (int) Key.End;
            io.NativePtr->KeyMap[(int) ImGuiKey.Delete] = (int) Key.Delete;
            io.NativePtr->KeyMap[(int) ImGuiKey.Backspace] = (int) Key.BackSpace;
            io.NativePtr->KeyMap[(int) ImGuiKey.Enter] = (int) Key.Enter;
            io.NativePtr->KeyMap[(int) ImGuiKey.Escape] = (int) Key.Escape;
            io.NativePtr->KeyMap[(int) ImGuiKey.A] = (int) Key.A;
            io.NativePtr->KeyMap[(int) ImGuiKey.C] = (int) Key.C;
            io.NativePtr->KeyMap[(int) ImGuiKey.V] = (int) Key.V;
            io.NativePtr->KeyMap[(int) ImGuiKey.X] = (int) Key.X;
            io.NativePtr->KeyMap[(int) ImGuiKey.Y] = (int) Key.Y;
            io.NativePtr->KeyMap[(int) ImGuiKey.Z] = (int) Key.Z;
        }

        protected unsafe void ImGui_Input_Update_Key(ImGuiIOPtr io)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            io.NativePtr->KeysDown[(int) Key.Tab] = BoolHelper.ToByte(keyboardState[Key.Tab]);
            io.NativePtr->KeysDown[(int) Key.Left] = BoolHelper.ToByte(keyboardState[Key.Left]);
            io.NativePtr->KeysDown[(int) Key.Right] = BoolHelper.ToByte(keyboardState[Key.Right]);
            io.NativePtr->KeysDown[(int) Key.Up] = BoolHelper.ToByte(keyboardState[Key.Up]);
            io.NativePtr->KeysDown[(int) Key.Down] = BoolHelper.ToByte(keyboardState[Key.Down]);
            io.NativePtr->KeysDown[(int) Key.PageUp] = BoolHelper.ToByte(keyboardState[Key.PageUp]);
            io.NativePtr->KeysDown[(int) Key.PageDown] = BoolHelper.ToByte(keyboardState[Key.PageDown]);
            io.NativePtr->KeysDown[(int) Key.Home] = BoolHelper.ToByte(keyboardState[Key.Home]);
            io.NativePtr->KeysDown[(int) Key.End] = BoolHelper.ToByte(keyboardState[Key.End]);
            io.NativePtr->KeysDown[(int) Key.Delete] = BoolHelper.ToByte(keyboardState[Key.Delete]);
            io.NativePtr->KeysDown[(int) Key.BackSpace] = BoolHelper.ToByte(keyboardState[Key.BackSpace]);
            io.NativePtr->KeysDown[(int) Key.Enter] = BoolHelper.ToByte(keyboardState[Key.Enter]);
            io.NativePtr->KeysDown[(int) Key.Escape] = BoolHelper.ToByte(keyboardState[Key.Escape]);
            io.NativePtr->KeysDown[(int) Key.A] = BoolHelper.ToByte(keyboardState[Key.A]);
            io.NativePtr->KeysDown[(int) Key.C] = BoolHelper.ToByte(keyboardState[Key.C]);
            io.NativePtr->KeysDown[(int) Key.V] = BoolHelper.ToByte(keyboardState[Key.V]);
            io.NativePtr->KeysDown[(int) Key.X] = BoolHelper.ToByte(keyboardState[Key.X]);
            io.NativePtr->KeysDown[(int) Key.Y] = BoolHelper.ToByte(keyboardState[Key.Y]);
            io.NativePtr->KeysDown[(int) Key.Z] = BoolHelper.ToByte(keyboardState[Key.Z]);
        }

        protected unsafe void ImGui_Input_Update()
        {
            ImGuiIOPtr io = ImGui.GetIO();
            ImGui_Input_Update_Key(io);
            MouseState cursorState = Mouse.GetCursorState();

            MouseState mouseState = Mouse.GetState();
            if (Focused)
            {
                Point windowPoint = PointToClient(new Point(cursorState.X, cursorState.Y));
                io.MousePos = new Vec2(windowPoint.X / io.DisplayFramebufferScale.X,
                    windowPoint.Y / io.DisplayFramebufferScale.Y);
            }

            else
            {
                io.MousePos = new Vec2(-1f, -1f);
            }

            io.NativePtr->MouseDown[0] = BoolHelper.ToByte(mouseState.LeftButton == ButtonState.Pressed);
            io.NativePtr->MouseDown[1] = BoolHelper.ToByte(mouseState.RightButton == ButtonState.Pressed);
            io.NativePtr->MouseDown[2] = BoolHelper.ToByte(mouseState.MiddleButton == ButtonState.Pressed);
            float newWheelPos = mouseState.WheelPrecise;
            float delta = newWheelPos - _mouseWheel;
            _mouseWheel = newWheelPos;
            io.MouseWheel += delta;
        }

        protected unsafe void ImGui_Render(FrameEventArgs ev)
        {
            ImGuiIOPtr io = ImGui.GetIO();
            Vec2 display = new Vec2(ClientSize.Width, ClientSize.Height);
            io.DisplaySize = display;
            io.DisplayFramebufferScale = Vec2.One;
            io.DeltaTime = (float) ev.Time;
            ImGui.NewFrame();
            foreach (IScene scene in _loadedScenes.Values)
            {
                scene.OnGUI(ev.Time);
            }

            ImGui.Render();

            if (io.RenderDrawListsFnUnused == IntPtr.Zero)
                ImGui_DataRender(ImGui.GetDrawData());
        }

        protected unsafe void ImGui_DataRender(ImDrawData* drawData)
        {
            ImGuiIOPtr io = ImGui.GetIO();
            int last_texture;
            GL.GetInteger(GetPName.TextureBinding2D, out last_texture);
            GL.PushAttrib(AttribMask.EnableBit | AttribMask.ColorBufferBit | AttribMask.TransformBit);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Disable(EnableCap.CullFace);
            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ScissorTest);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.EnableClientState(ArrayCap.ColorArray);
            GL.Enable(EnableCap.Texture2D);
            GL.UseProgram(0);
            ImGui.GetDrawData().ScaleClipRects(io.DisplayFramebufferScale);
            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Ortho(
                0.0f,
                io.DisplaySize.X / io.DisplayFramebufferScale.X,
                io.DisplaySize.Y / io.DisplayFramebufferScale.Y,
                0.0f,
                -1.0f,
                1.0f);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            GL.LoadIdentity();
            for (int n = 0;
                n < drawData->CmdListsCount;
                n++)
            {
                ImDrawList* cmd_list = drawData->CmdLists[n];
                byte* vtx_buffer = (byte*) cmd_list->VtxBuffer.Data;
                ushort* idx_buffer = (ushort*) cmd_list->IdxBuffer.Data;

                /*ImDrawVert vert0 = *((ImDrawVert*) vtx_buffer);
                ImDrawVert vert1 = *(((ImDrawVert*) vtx_buffer) + 1);
                ImDrawVert vert2 = *(((ImDrawVert*) vtx_buffer) + 2);*/

                GL.VertexPointer(2, VertexPointerType.Float, sizeof(ImDrawVert),
                    new IntPtr(vtx_buffer + 0));
                GL.TexCoordPointer(2, TexCoordPointerType.Float, sizeof(ImDrawVert),
                    new IntPtr(vtx_buffer + 8));
                GL.ColorPointer(4, ColorPointerType.UnsignedByte, sizeof(ImDrawVert),
                    new IntPtr(vtx_buffer + 16));

                for (int cmd_i = 0; cmd_i < cmd_list->CmdBuffer.Size; cmd_i++)
                {
                    ImDrawCmd* pcmd = &(((ImDrawCmd*) cmd_list->CmdBuffer.Data)[cmd_i]);
                    if (pcmd->UserCallback != IntPtr.Zero)
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        GL.BindTexture(TextureTarget.Texture2D, pcmd->TextureId.ToInt32());
                        GL.Scissor(
                            (int) (pcmd->ClipRect.X),
                            (int) (io.DisplaySize.Y - pcmd->ClipRect.W),
                            (int) (pcmd->ClipRect.Z - pcmd->ClipRect.X),
                            (int) (pcmd->ClipRect.W - pcmd->ClipRect.Y));
                        ushort[] indices = new ushort[pcmd->ElemCount];
                        for (int i = 0; i < indices.Length; i++)
                        {
                            indices[i] = idx_buffer[i];
                        }

                        GL.DrawElements(PrimitiveType.Triangles, (int) pcmd->ElemCount, DrawElementsType.UnsignedShort,
                            new IntPtr(idx_buffer));
                    }

                    idx_buffer += pcmd->ElemCount;
                }
            }

            GL.DisableClientState(ArrayCap.ColorArray);
            GL.DisableClientState(ArrayCap.TextureCoordArray);
            GL.DisableClientState(ArrayCap.VertexArray);
            GL.BindTexture(TextureTarget.Texture2D, last_texture);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PopMatrix();
            GL.MatrixMode(MatrixMode.Projection);
            GL.PopMatrix();
            GL.PopAttrib();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            try
            {
                base.OnUpdateFrame(e);
                ImGui_Input_Update();
                foreach (IScene scene in _loadedScenes.Values)
                {
                    scene.OnUpdate(e.Time);
                }
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
                foreach (IScene scene in _loadedScenes.Values)
                {
                    scene.OnDraw(e.Time);
                }

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

            Logger = LogManager.GetCurrentClassLogger();
            OnInitialze();
            base.OnLoad(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            ImGui.GetIO().AddInputCharacter(e.KeyChar);
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
                if (ExceptionDialog)
                    Dialog.Open("Exception", exception.ToString(), Dialog.DialogType.ICON_ERROR);
                else
                    Dialog.Open("Exception", exception.Message, Dialog.DialogType.ICON_ERROR);
            }
        }

        public void Exit(string message)
        {
            Exit();
            Dialog.Open("Error", message, Dialog.DialogType.ICON_ERROR);
        }
    }
}
using System;
using System.Drawing;
using OpenTK;

namespace EngineGL.Editor.Impl
{
    public class GameWindowHandler : IGameWindowHandler
    {
        public Game Game { get; } = new Game();

        public void Load(IntPtr windowPtr)
        {
            Game.OnLoad(EventArgs.Empty);
            Game.LoadDefaultFunc(windowPtr);
        }

        public void Render(bool focused, Point clientPoint, Size clientSize)
        {
            Game.OnUpdateFrame(new FrameEventArgs(), focused, clientPoint);
            Game.DrawDefaultFunc(new FrameEventArgs(), clientSize);
            Game.OnRenderFrame(new FrameEventArgs());
        }

        public void Resize(Rectangle clientRectangle)
        {
            Game.AdjustResize(clientRectangle);
        }
    }
}
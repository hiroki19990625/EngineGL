using System;
using System.Drawing;
using EngineGL.Impl;
using EngineGL.Impl.Drawable;
using EngineGL.Impl.Objects;
using EngineGL.Structs.Math;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Editor
{
    public class FormEditor
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
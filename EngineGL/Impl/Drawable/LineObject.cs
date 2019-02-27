using System;
using EngineGL.Core.LifeCycle;
using EngineGL.Event.LifeCycle;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable
{
    public class LineObject : GameObject, IDrawable
    {
        public Color4 LineColor { get; set; }
        public float LineWidth { get; set; } = 1;

        public event EventHandler<DrawEventArgs> Draw;

        public virtual void OnDraw()
        {
            CallDrawEvent();

            GL.LineWidth(LineWidth);

            GL.Begin(PrimitiveType.Lines);
            GL.Color4(LineColor);
            GL.Vertex3(Position);
            GL.Vertex3(Bounds);
            GL.End();
        }

        /*public override void OnUpdate()
        {
            Position += new Vector3(0.001f, 0.001f, 0);
            Bounds -= new Vector3(0.001f, 0.001f, 0);
        }*/

        protected void CallDrawEvent()
        {
            Draw?.Invoke(this, new DrawEventArgs(this));
        }
    }
}
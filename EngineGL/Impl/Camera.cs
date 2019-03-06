using System;
using EngineGL.Core;
using EngineGL.Core.LifeCycle;
using EngineGL.Event.LifeCycle;
using OpenTK;

namespace EngineGL.Impl
{
    public abstract class Camera : GameObject, ICamera, IDrawable
    {
        [NonSerialized] protected Matrix4 _lookAtMatrix;
        public Matrix4 LookAtMatrix => _lookAtMatrix;

        public event EventHandler<DrawEventArgs> Draw;

        public virtual void OnDraw()
        {
            Draw?.Invoke(this, new DrawEventArgs(this));
        }
    }
}
using System;
using EngineGL.Core.LifeCycle;
using EngineGL.Event.LifeCycle;
using EngineGL.Impl.Components;
using EngineGL.Serializations.Resulter;
using EngineGL.Structs.Drawing;
using Newtonsoft.Json;
using OpenTK;

namespace EngineGL.Impl.Objects
{
    public abstract class Camera : Component, IDrawable
    {
        protected Matrix4 _lookAtMatrix;
        [SerializeIgnore, JsonIgnore] public Matrix4 LookAtMatrix => _lookAtMatrix;

        public uint Layer { get; }
        public Colour4 Colour { get; set; }

        public event EventHandler<DrawEventArgs> Draw;

        public virtual void OnDraw(double deltaTime)
        {
            Draw?.Invoke(this, new DrawEventArgs(this, deltaTime));
        }
    }
}
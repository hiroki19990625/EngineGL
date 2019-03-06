using System;
using EngineGL.Core;
using EngineGL.Core.LifeCycle;
using EngineGL.Event.LifeCycle;
using Newtonsoft.Json;
using OpenTK;
using YamlDotNet.Serialization;

namespace EngineGL.Impl
{
    public abstract class Camera : GameObject, ICamera, IDrawable
    {
        protected Matrix4 _lookAtMatrix;
        [JsonIgnore, YamlIgnore] public Matrix4 LookAtMatrix => _lookAtMatrix;

        public event EventHandler<DrawEventArgs> Draw;

        public virtual void OnDraw()
        {
            Draw?.Invoke(this, new DrawEventArgs(this));
        }
    }
}
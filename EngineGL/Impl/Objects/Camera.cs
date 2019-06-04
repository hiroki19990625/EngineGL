using System;
using EngineGL.Core;
using EngineGL.Core.LifeCycle;
using EngineGL.Event.LifeCycle;
using Newtonsoft.Json;
using OpenTK;
using YamlDotNet.Serialization;

namespace EngineGL.Impl.Objects
{
    public abstract class Camera : GameObject, ICamera, IDrawable
    {

        /// <summary>
        /// 描画レイヤー
        /// </summary>
        public uint Layer { get; set; } = 0;

        protected Matrix4 _lookAtMatrix;
        [JsonIgnore, YamlIgnore] public Matrix4 LookAtMatrix => _lookAtMatrix;

        public event EventHandler<DrawEventArgs> Draw;

        public virtual void OnDraw(double deltaTime)
        {
            Draw?.Invoke(this, new DrawEventArgs(this, deltaTime));
        }
    }
}
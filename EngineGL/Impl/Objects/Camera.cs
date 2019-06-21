using System;
using EngineGL.Core;
using EngineGL.Core.LifeCycle;
using EngineGL.Event.LifeCycle;
using EngineGL.Structs.Drawing;
using EngineGL.Serializations.Resulter;
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
        [SerializeIgnore, JsonIgnore] public Matrix4 LookAtMatrix => _lookAtMatrix;

        //いずれこのプロパティは破棄する
        public Colour4 Colour { get; set; }

        public event EventHandler<DrawEventArgs> Draw;

        public virtual void OnDraw(double deltaTime)
        {
            Draw?.Invoke(this, new DrawEventArgs(this, deltaTime));
        }
    }
}
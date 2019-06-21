using System;
using EngineGL.Event.LifeCycle;
using EngineGL.GraphicAdapter;
using EngineGL.GraphicAdapter.Interface;
using EngineGL.Impl.DrawableComponents;
using EngineGL.Serializations.Resulter;
using EngineGL.Structs.Drawing;
using Newtonsoft.Json;
using OpenTK;

namespace EngineGL.Impl.Objects
{
    public abstract class Camera : DrawableComponent
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

        protected Camera(IGraphicAdapter graphicAdapter) : base(graphicAdapter)
        {
        }

        public virtual void OnDraw(double deltaTime)
        {
            Draw?.Invoke(this, new DrawEventArgs(this, deltaTime));
        }
    }
}
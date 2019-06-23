using System;
using EngineGL.Core.Components;
using EngineGL.Event.LifeCycle;
using EngineGL.GraphicAdapter.Interface;
using EngineGL.Impl.Components;
using EngineGL.Structs.Drawing;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.DrawableComponents
{
    public class DrawableComponent : Component, IDrawableComponent
    {
        private IGraphicAdapter _graphicAdapter;
        private Colour4 _colour;

        /// <summary>
        /// 描画レイヤー
        /// </summary>
        public uint Layer { get; set; } = 0;

        public Colour4 Colour
        {
            get => _colour;
            set
            {
                _colour = value;
                _graphicAdapter.SetColour4(_colour);
            }
        }

        public event EventHandler<DrawEventArgs> Draw;

        public DrawableComponent(IGraphicAdapter graphicAdapter)
        {
            _graphicAdapter = graphicAdapter;
            _graphicAdapter.PreprocessVertexFunc = OnPreprocessVertex;
            _graphicAdapter.VertexWriteFunc = OnVertexWrite;
        }

        public virtual void OnDraw(double deltaTime)
        {
            CallDrawEvent(deltaTime);
            _graphicAdapter.Draw(deltaTime);
        }

        /// <summary>
        /// 頂点書き込み処理をする関数
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <param name="vertexHandler"></param>
        public virtual void OnVertexWrite(double deltaTime, IVertexHandler vertexHandler)
        {
        }


        /// <summary>
        /// 頂点書き込み前処理をする関数
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <param name="matrixHandler"></param>
        public virtual void OnPreprocessVertex(double deltaTime, IPreprocessVertexHandler matrixHandler)
        {
            //オイラー回転(Translateを使ってオブジェクトの原点に平行移動してから回転し、再び平行移動で元の位置に戻す)
            GL.Translate(GameObject.Transform.Position + GameObject.Transform.Bounds / 2);
            GL.Rotate(GameObject.Transform.Rotation.Y, 0, 1, 0);
            GL.Rotate(GameObject.Transform.Rotation.Z, 0, 0, 1);
            GL.Rotate(GameObject.Transform.Rotation.X, 1, 0, 0);
            GL.Translate((GameObject.Transform.Position + GameObject.Transform.Bounds / 2) * -1);
            GL.Translate(GameObject.Transform.Position);
        }

        protected void CallDrawEvent(double deltaTime)
        {
            Draw?.Invoke(this, new DrawEventArgs(this, deltaTime));
        }
    }
}
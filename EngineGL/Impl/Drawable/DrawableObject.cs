using System;
using EngineGL.Core.LifeCycle;
using EngineGL.Event.LifeCycle;
using EngineGL.GraphicAdapter;

namespace EngineGL.Impl.Drawable
{
    public abstract class DrawableObject : GameObject, IDrawable
    {

        private IGraphicAdapter _graphicAdapter;

        /// <summary>
        /// 描画レイヤー
        /// </summary>
        public uint Layer { get; set; } = 0;

        public event EventHandler<DrawEventArgs> Draw;

        public DrawableObject(IGraphicAdapter graphicAdapter)
        {
            _graphicAdapter = graphicAdapter;
            _graphicAdapter.PreprocessVertexFunc =  OnPreprocessVertex;
            _graphicAdapter.VertexWriteFunc = OnVertexWrite;
        }

        public void OnDraw(double deltaTime)
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
        public virtual void OnPreprocessVertex(double deltaTime,IPreprocessVertexHandler matrixHandler)
        {
        }

        protected void CallDrawEvent(double deltaTime)
        {
            Draw?.Invoke(this, new DrawEventArgs(this, deltaTime));
        }
    }
}
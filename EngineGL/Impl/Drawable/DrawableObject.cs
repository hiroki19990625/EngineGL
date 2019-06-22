using System;
using EngineGL.Core.LifeCycle;
using EngineGL.Event.LifeCycle;
using EngineGL.GraphicAdapter;
using EngineGL.Structs.Drawing;

namespace EngineGL.Impl.Drawable
{
    public abstract class DrawableObject : GameObject, IDrawable
    {

        private IGraphicAdapter _graphicAdapter;

        /// <summary>
        /// 描画レイヤー
        /// </summary>
        public uint Layer { get; set; } = 0;
        public Colour4 Colour { get; set; } = new Colour4(255, 255, 255);

        public event EventHandler<DrawEventArgs> Draw;

        public DrawableObject(IGraphicAdapter graphicAdapter)
        {
            _graphicAdapter = graphicAdapter;
            _graphicAdapter.PreprocessVertexFunc = OnPreprocessVertex;
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
        public virtual void OnPreprocessVertex(double deltaTime, IPreprocessVertexHandler preprocessVertexHandler)
        {
            //オイラー回転
            //Translateを使ってオブジェクトの原点に平行移動してから回転し、再び平行移動で元の位置に戻す
            preprocessVertexHandler.Translate(Transform.Position + Transform.Bounds / 2);
            preprocessVertexHandler.Euler(Transform.Rotation);
            preprocessVertexHandler.Translate((Transform.Position + Transform.Bounds / 2) * -1);
            preprocessVertexHandler.Translate(Transform.Position);

            //カラーセット
            preprocessVertexHandler.SetColour(Colour);
        }

        protected void CallDrawEvent(double deltaTime)
        {
            Draw?.Invoke(this, new DrawEventArgs(this, deltaTime));
        }
    }
}
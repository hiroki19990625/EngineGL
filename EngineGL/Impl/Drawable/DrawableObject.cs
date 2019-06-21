using System;
using EngineGL.Core.LifeCycle;
using EngineGL.Event.LifeCycle;
using EngineGL.GraphicAdapter;
using EngineGL.Structs.Drawing;
using OpenTK.Graphics.OpenGL;

namespace EngineGL.Impl.Drawable
{
    public abstract class DrawableObject : GameObject, IDrawable
    {

        private IGraphicAdapter _graphicAdapter;
        private Colour4 _colour;

        /// <summary>
        /// 描画レイヤー
        /// </summary>
        public uint Layer { get; set; } = 0;
        public Colour4 Colour { get => _colour; set { _colour = value; _graphicAdapter.SetColour4(_colour); } }

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
            //オイラー回転(Translateを使ってオブジェクトの原点に平行移動してから回転し、再び平行移動で元の位置に戻す)
            GL.Translate(Transform.Position + Transform.Bounds / 2);
            GL.Rotate(Transform.Rotation.Y, 0, 1, 0);
            GL.Rotate(Transform.Rotation.Z, 0, 0, 1);
            GL.Rotate(Transform.Rotation.X, 1, 0, 0);
            GL.Translate((Transform.Position + Transform.Bounds / 2) * -1);
            GL.Translate(Transform.Position );
        }

        protected void CallDrawEvent(double deltaTime)
        {
            Draw?.Invoke(this, new DrawEventArgs(this, deltaTime));
        }
    }
}
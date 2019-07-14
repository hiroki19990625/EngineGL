using System;
using EngineGL.Core.Components;
using EngineGL.Event.LifeCycle;
using EngineGL.GraphicAdapter.Interface;
using EngineGL.Impl.Components;
using EngineGL.Structs.Drawing;

namespace EngineGL.Impl.DrawableComponents
{
    public class DrawableComponent : Component, IDrawableComponent
    {
        private IGraphicAdapter _graphicAdapter;

        /// <summary>
        /// 描画レイヤー
        /// </summary>
        public uint Layer { get; set; } = 0;

        public Colour4 Colour { get; set; } = new Colour4(255, 255, 255);

        public event EventHandler<DrawEventArgs> Draw;

        public DrawableComponent(IGraphicAdapter graphicAdapter)
        {
            _graphicAdapter = graphicAdapter;
            _graphicAdapter.SettingFunc = OnGraphicSetting;
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
        /// <param name="settingHandler"></param>
        public virtual void OnGraphicSetting(double deltaTime, ISettingHandler settingHandler)
        {
            //オイラー回転
            //Translateを使ってオブジェクトの原点に平行移動してから回転し、再び平行移動で元の位置に戻す
            settingHandler.Translate(GameObject.Transform.Position);
            settingHandler.Euler(GameObject.Transform.LocalRotation);
            settingHandler.Translate((GameObject.Transform.Position) * -1);
            settingHandler.Translate(GameObject.Transform.Position);

            //カラーセット
            settingHandler.SetColour(Colour);
        }

        protected void CallDrawEvent(double deltaTime)
        {
            Draw?.Invoke(this, new DrawEventArgs(this, deltaTime));
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _graphicAdapter.Dispose();
        }
    }
}
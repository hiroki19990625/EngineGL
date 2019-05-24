using System;
using EngineGL.Event.LifeCycle;

namespace EngineGL.Core.LifeCycle
{
    /// <summary>
    /// GUIを描画する時のイベントとメソッドを実装します。
    /// </summary>
    public interface IGuiRenderable
    {
        /// <summary>
        /// GUIを描画する時に発火するイベント
        /// </summary>
        event EventHandler<GuiRenderEventArgs> GuiRender;

        /// <summary>
        /// GUIを描画するメソッド
        /// </summary>
        /// <param name="deltaTime">ゲームのフレーム更新間隔時間</param>
        void OnGUI(double deltaTime);
    }
}
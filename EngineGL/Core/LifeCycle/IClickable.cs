using System;
using EngineGL.Event.LifeCycle;
using EngineGL.Structs.Drawing;

namespace EngineGL.Core.LifeCycle
{
    public interface IClickable
    {
        event EventHandler<ClickEventArgs> Draw;

        /// <summary>
        /// オブジェクトを描画するメソッド
        /// </summary>
        /// <param name="deltaTime">ゲームのフレーム更新間隔時間</param>
        void OnClick();
    }
}
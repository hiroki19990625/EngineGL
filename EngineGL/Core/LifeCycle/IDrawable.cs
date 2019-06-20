using System;
using EngineGL.Event.LifeCycle;
using EngineGL.Structs.Drawing;

namespace EngineGL.Core.LifeCycle
{
    /// <summary>
    /// オブジェクトを描画する時のイベントとメソッドを実装します。
    /// 描画関数を直接呼び出し、オブジェクトを描画することができます。
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// オブジェクトが描画する時に発火するイベント
        /// </summary>
        event EventHandler<DrawEventArgs> Draw;

        /// <summary>
        /// オブジェクトを描画するメソッド
        /// </summary>
        /// <param name="deltaTime">ゲームのフレーム更新間隔時間</param>
        void OnDraw(double deltaTime);

        /// <summary>
        /// 描画レイヤー
        /// </summary>
        uint Layer { get; }

        Colour4 Colour { get; set; }
    }
}
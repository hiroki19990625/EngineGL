using System;
using EngineGL.Event.LifeCycle;

namespace EngineGL.Core.LifeCycle
{
    /// <summary>
    /// オブジェクトが更新された時のイベントとメソッドを実装します。
    /// </summary>
    public interface IUpdateable
    {
        /// <summary>
        /// オブジェクトが更新された時に発火するイベント
        /// </summary>
        event EventHandler<UpdateEventArgs> Update;

        /// <summary>
        /// オブジェクトが更新された時に呼び出されるメソッド
        /// </summary>
        /// <param name="deltaTime">ゲームのフレーム更新間隔時間</param>
        void OnUpdate(double deltaTime);
    }
}
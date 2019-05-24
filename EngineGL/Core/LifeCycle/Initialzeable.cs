using System;
using EngineGL.Event.LifeCycle;

namespace EngineGL.Core.LifeCycle
{
    /// <summary>
    /// オブジェクトが初期化された時のイベントとメソッドを実装します。
    /// </summary>
    public interface Initialzeable
    {
        /// <summary>
        /// オブジェクトが初期化された時に発火するイベント
        /// </summary>
        event EventHandler<InitialzeEventArgs> Initialze;

        /// <summary>
        /// オブジェクトが初期化された時に呼び出されるメソッド
        /// </summary>
        void OnInitialze();
    }
}
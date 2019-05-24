using System;
using EngineGL.Event.LifeCycle;

namespace EngineGL.Core.LifeCycle
{
    /// <summary>
    /// オブジェクトが破棄された時のイベントとメソッドを実装します。
    /// </summary>
    public interface IDestroyable
    {
        /// <summary>
        /// オブジェクトが破棄された時に発火するイベント
        /// </summary>
        event EventHandler<DestroyEventArgs> Destroy;

        /// <summary>
        /// オブジェクトが破棄された時に呼び出されるメソッド
        /// </summary>
        void OnDestroy();
    }
}
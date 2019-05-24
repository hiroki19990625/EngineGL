using System;
using EngineGL.Core.LifeCycle;

namespace EngineGL.Core
{
    /// <summary>
    /// <see cref="IScene"/>に追加できるオブジェクトを実装します。
    /// </summary>
    public interface IObject : Initialzeable, IDestroyable, IUpdateable, INameable
    {
        /// <summary>
        /// このオブジェクトが存在するシーン。
        /// </summary>
        IScene Scene { get; set; }

        /// <summary>
        /// このオブジェクトの検索用タグ。
        /// </summary>
        string Tag { get; set; }

        /// <summary>
        /// このオブジェクトのインスタンスID。
        /// </summary>
        Guid InstanceGuid { get; }
    }
}
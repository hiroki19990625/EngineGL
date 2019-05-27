using System;
using EngineGL.Core.LifeCycle;

namespace EngineGL.Core
{
    /// <summary>
    /// <see cref="IComponentAttachable"/> にアタッチ可能なコンポーネントを実装します。
    /// </summary>
    public interface IComponent : Initialzeable, IUpdateable, IDestroyable
    {
        /// <summary>
        /// コンポーネントがアタッチしているオブジェクト
        /// </summary>
        IComponentAttachable ParentObject { get; set; }

        /// <summary>
        /// コンポーネントがアタッチしている <see cref="IGameObject"/> オブジェクト
        /// </summary>
        IGameObject GameObject { get; }

        /// <summary>
        /// コンポーネントのインスタンスID
        /// </summary>
        Guid InstanceGuid { get; }
    }
}
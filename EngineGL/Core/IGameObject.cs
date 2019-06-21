using EngineGL.Core.Components;

namespace EngineGL.Core
{
    /// <summary>
    /// <see cref="IScene"/> の空間に配置でき、コンポーネントをアタッチ出来るオブジェクトを実装します。
    /// </summary>
    public interface IGameObject : IObject, IComponentAttachable
    {
        /// <summary>
        /// ゲームオブジェクトが持つITransformを返します
        /// </summary>
        ITransform Transform { get; }

        /// <summary>
        /// ゲームオブジェクトをTransformの子として追加
        /// Sceneにも登録してくれます
        /// </summary>
        /// <param name="gameObject">
        /// 子として追加するゲームオブジェクト
        /// </param>
        void AddChild(IGameObject gameObject);
    }
}
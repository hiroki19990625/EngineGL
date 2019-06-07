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
    }
}
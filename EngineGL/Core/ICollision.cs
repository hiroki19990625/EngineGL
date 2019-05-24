namespace EngineGL.Core
{
    /// <summary>
    /// ゲームオブジェクト同士の当たり判定を実装します。
    /// </summary>
    public interface ICollision : IComponent
    {
        /// <summary>
        /// ゲームオブジェクト同士接触した時に呼び出されるメソッド
        /// </summary>
        /// <param name="gameObject">接触したオブジェクト</param>
        void OnCollisionEnter(IGameObject gameObject);

        /// <summary>
        /// ゲームオブジェクト同士が接触している時に呼び出されるメソッド
        /// </summary>
        /// <param name="gameObject">接触しているオブジェクト</param>
        void OnCollisionStay(IGameObject gameObject);

        /// <summary>
        /// ゲームオブジェクト同士が離れた時に呼び出されるメソッド
        /// </summary>
        /// <param name="gameObject">離れたオブジェクト</param>
        void OnCollisionLeave(IGameObject gameObject);
    }
}
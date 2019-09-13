namespace EngineGL.Security.Secure
{
    /// <summary>
    /// データ型をメモリ上で検索されにくくする機能を実装します。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISecureValue<T>
    {
        /// <summary>
        /// ハッシュ化する際のシード値
        /// </summary>
        byte[] Seed { get; }

        /// <summary>
        /// データを復元します。
        /// </summary>
        T Value { get; }

        /// <summary>
        /// 値を設定し、検索されにくくします。
        /// </summary>
        /// <param name="value"></param>
        void Set(T value);
    }
}
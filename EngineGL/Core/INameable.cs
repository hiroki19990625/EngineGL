namespace EngineGL.Core
{
    /// <summary>
    /// 名前付きオブジェクトを実装します。
    /// </summary>
    public interface INameable
    {
        /// <summary>
        /// オブジェクトの名前
        /// </summary>
        string Name { get; set; }
    }
}
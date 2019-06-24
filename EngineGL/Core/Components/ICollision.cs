using EngineGL.Structs.Math;

namespace EngineGL.Core.Components
{
    /// <summary>
    /// 当たり判定の範囲を実装します。
    /// </summary>
    public interface ICollision : IComponent
    {
        /// <summary>
        /// 当たり判定のサイズ
        /// </summary>
        Vec3 Bounds { get; set; }
        /// <summary>
        /// 当たり判定のオフセット
        /// </summary>
        Vec3 Offset { get; set; }
    }
}
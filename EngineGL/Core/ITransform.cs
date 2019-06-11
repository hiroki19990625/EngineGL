using EngineGL.Structs.Math;

namespace EngineGL.Core
{
    /// <summary>
    /// オブジェクトの空間座標と、回転、スケールを実装します。
    /// </summary>
    public interface ITransform : IComponent
    {
        /// <summary>
        /// オブジェクトの空間座標
        /// </summary>
        Vec3 Position { get; set; }

        /// <summary>
        /// オブジェクトの回転
        /// </summary>
        Vec3 Rotation { get; set; }

        /// <summary>
        /// オブジェクトの境界
        /// </summary>
        Vec3 Bounds { get; set; }

        /// <summary>
        /// オブジェクトのスケール
        /// </summary>
        Vec3 Scale { get; set; }
    }
}
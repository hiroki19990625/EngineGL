using EngineGL.Core.Components;
using OpenTK;

namespace EngineGL.Core
{
    /// <summary>
    /// ゲームで使用するカメラを実装します。
    /// </summary>
    public interface ICamera : IDrawableComponent
    {
        /// <summary>
        /// カメラがターゲットしている行列
        /// </summary>
        Matrix4 LookAtMatrix { get; }
    }
}
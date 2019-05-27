using EngineGL.Core.LifeCycle;
using OpenTK;

namespace EngineGL.Core
{
    /// <summary>
    /// ゲームで使用するカメラを実装します。
    /// </summary>
    public interface ICamera : IUpdateable
    {
        /// <summary>
        /// カメラがターゲットしている行列
        /// </summary>
        Matrix4 LookAtMatrix { get; }
    }
}
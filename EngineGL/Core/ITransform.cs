using EngineGL.Structs;
using EngineGL.Structs.Math;

namespace EngineGL.Core
{
    /// <summary>
    /// �I�u�W�F�N�g�̋�ԍ��W�ƁA��]�A�X�P�[�����������܂��B
    /// </summary>
    public interface ITransform
    {
        /// <summary>
        /// �I�u�W�F�N�g�̋�ԍ��W
        /// </summary>
        Vec3 Position { get; set; }

        /// <summary>
        /// �I�u�W�F�N�g�̉�]
        /// </summary>
        Vec3 Rotation { get; set; }

        /// <summary>
        /// �I�u�W�F�N�g�̋��E
        /// </summary>
        Vec3 Bounds { get; set; }

        /// <summary>
        /// �I�u�W�F�N�g�̃X�P�[��
        /// </summary>
        Vec3 Scale { get; set; }
    }
}
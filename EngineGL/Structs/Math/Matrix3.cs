using System;
using System.Collections.Generic;
using System.Linq;
using Net= MathNet.Numerics.LinearAlgebra;

namespace EngineGL.Structs.Math
{
    /// <summary>
    /// <see cref="float"/>型の3x3行列を表します
    /// </summary>
    class Matrix3 : IEquatable<Matrix3>
    {
        private Net.Matrix<float> _matrix;
        public const int SIZE = 3;

        public Matrix3(params Vec3[] vec3s)
        {
            if (vec3s.Length != SIZE) throw new ArgumentException("vec3s.Length!=3");
            _matrix= Net.CreateMatrix.DenseOfColumns(vec3s.Select(v => new float[] { v.X, v.Y, v.Z }));
        }

        public Matrix3(IEnumerable<Vec3> vec3s)
        {
            Vec3[] vec3Array = vec3s.ToArray();
            if (vec3Array.Length != SIZE) throw new ArgumentException("vec3s.Length!=3");
            _matrix = Net.CreateMatrix.DenseOfColumns(vec3Array.Select(v => new float[] { v.X, v.Y, v.Z }));
        }

        /// <summary>
        /// 行列の各要素が等しいか判定します
        /// </summary>
        /// <param name="other"></param>
        /// <returns>判定結果</returns>
        public bool Equals(Matrix3 other)
            => _matrix == other._matrix;

        /// <summary>
        /// 逆行列を返します
        /// </summary>
        /// <returns>生成された逆行列</returns>
        public Matrix3 Inverse()
            => new Matrix3(_matrix.Inverse());

        /// <summary>
        /// 線形写像にベクトルを適用します
        /// </summary>
        /// <param name="matrix3"></param>
        /// <param name="b"></param>
        /// <returns>写された後のベクトル</returns>
        public static Vec3 operator *(Matrix3 matrix3, Vec3 b)
        {
            Net.Vector<float> vec = matrix3._matrix * Net.CreateVector.DenseOfArray(new float[] { b.X, b.Y, b.Z });
            return new Vec3(vec[0], vec[1], vec[2]);
        }

        /// <summary>
        /// 線形写像同士を合成します
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>合成後の線形写像</returns>
        public static Matrix3 operator *(Matrix3 a, Matrix3 b)
            => new Matrix3(a._matrix * b._matrix);

        /// <summary>
        /// 行列をスカラー倍します
        /// </summary>
        /// <param name="matrix3"></param>
        /// <param name="f"></param>
        /// <returns>スカラー倍した後の行列</returns>
        public static Matrix3 operator *(Matrix3 matrix3, float f)
            => new Matrix3(matrix3._matrix * f);

        /// <summary>
        /// 行列をスカラー倍します
        /// </summary>
        /// <param name="matrix3"></param>
        /// <param name="f"></param>
        /// <returns>スカラー倍した後の行列</returns>
        public static Matrix3 operator *(float f, Matrix3 matrix3)
            => matrix3 * f;

        private Matrix3(Net.Matrix<float> matrix)
            => _matrix = matrix;

    }
}

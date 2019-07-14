using System;
using System.Collections.Generic;
using System.Linq;
using Net= MathNet.Numerics.LinearAlgebra;

namespace EngineGL.Structs.Math
{
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

        public bool Equals(Matrix3 other)
            => _matrix == other._matrix;

        public Matrix3 Inverse()
            => new Matrix3(_matrix.Inverse());

        public static Vec3 operator *(Matrix3 matrix3, Vec3 b)
        {
            Net.Vector<float> vec = matrix3._matrix * Net.CreateVector.DenseOfArray(new float[] { b.X, b.Y, b.Z });
            return new Vec3(vec[0], vec[1], vec[2]);
        }

        public static Matrix3 operator *(Matrix3 a, Matrix3 b)
            => new Matrix3(a._matrix * b._matrix);

        public static Matrix3 operator *(Matrix3 matrix3, float f)
            => new Matrix3(matrix3._matrix * f);

        public static Matrix3 operator *(float f, Matrix3 matrix3)
            => matrix3 * f;

        private Matrix3(Net.Matrix<float> matrix)
            => _matrix = matrix;

    }
}

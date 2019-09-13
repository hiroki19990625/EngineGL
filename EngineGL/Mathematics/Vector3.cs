using System;
using Assimp;
using Jitter.LinearMath;
using Newtonsoft.Json;

namespace EngineGL.Mathematics
{
    /// <summary>
    /// 3つの <see cref="float"/> を持つベクトルを表します。
    /// </summary>
    [Serializable]
    public struct Vector3 : IEquatable<Vector3>
    {
        public static Vector3 Zero { get; } = new Vector3(0f, 0f, 0f);
        public static Vector3 One { get; } = new Vector3(1f, 1f, 1f);

        public static Vector3 Up { get; } = new Vector3(0f, 1f, 0f);
        public static Vector3 Down { get; } = new Vector3(0f, -1f, 0f);
        public static Vector3 Right { get; } = new Vector3(1f, 0f, 0f);
        public static Vector3 Left { get; } = new Vector3(-1f, 0f, 0f);

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        [JsonIgnore] public float Magnitude => (float) System.Math.Sqrt(SqrMagnitude);

        [JsonIgnore]
        public Vector3 Normalized
        {
            get
            {
                var m = Magnitude;
                return new Vector3(X / m, Y / m, Z / m);
            }
        }

        [JsonIgnore] public float SqrMagnitude => X * X + Y * Y + Z * Z;

        public Vector3(float value)
        {
            X = value;
            Y = value;
            Z = value;
        }

        public Vector3(Vector2 vec2)
        {
            X = vec2.X;
            Y = vec2.Y;
            Z = 0f;
        }

        public Vector3(float x, float y)
        {
            X = x;
            Y = y;
            Z = 0f;
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float Dot(Vector3 other) => X * other.X + Y * other.Y + Z * other.Z;

        public Vector3 Cross(Vector3 other) =>
            new Vector3(Y * other.Z - Z * other.Y, Z * other.X - X * other.Z, X * other.Y - Y * other.X);

        public bool Equals(Vector3 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector3 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"EngineGL.Structs.Math.Vec3: <X: {X}, Y: {Y}, Z: {Z}>";
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.X, -a.Y, -a.Z);
        }

        public static Vector3 operator *(float a, Vector3 b)
        {
            return new Vector3(a * b.X, a * b.Y, a * b.Z);
        }

        public static Vector3 operator *(Vector3 a, float b)
        {
            return new Vector3(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vector3 operator /(float a, Vector3 b)
        {
            return new Vector3(a / b.X, a / b.Y, a / b.Z);
        }

        public static Vector3 operator /(Vector3 a, float b)
        {
            return new Vector3(a.X / b, a.Y / b, a.Z / b);
        }

        public static bool operator ==(Vector3 a, Vector3 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return !a.Equals(b);
        }

        public static implicit operator Vector3(OpenTK.Vector3 a)
        {
            return new Vector3(a.X, a.Y, a.Z);
        }

        public static explicit operator Vector3(OpenTK.Vector4 a)
        {
            return new Vector3(a.X, a.Y, a.Z);
        }

        public static implicit operator Vector3(System.Numerics.Vector3 a)
        {
            return new Vector3(a.X, a.Y, a.Z);
        }

        public static explicit operator Vector3(System.Numerics.Vector4 a)
        {
            return new Vector3(a.X, a.Y, a.Z);
        }

        public static implicit operator Vector3(Vector3D a)
        {
            return new Vector3(a.X, a.Y, a.Z);
        }

        public static implicit operator Vector3(Vector2 a)
        {
            return new Vector3(a.X, a.Y);
        }

        public static explicit operator Vector3(Vector4 a)
        {
            return new Vector3(a.X, a.Y, a.Z);
        }

        public static implicit operator Vector3(JVector a)
        {
            return new Vector3(a.X, a.Y, a.Z);
        }

        public static implicit operator OpenTK.Vector3(Vector3 a)
        {
            return new OpenTK.Vector3(a.X, a.Y, a.Z);
        }

        public static implicit operator System.Numerics.Vector3(Vector3 a)
        {
            return new System.Numerics.Vector3(a.X, a.Y, a.Z);
        }

        public static implicit operator Vector3D(Vector3 a)
        {
            return new Vector3D(a.X, a.Y, a.Z);
        }

        public static implicit operator JVector(Vector3 a)
        {
            return new JVector(a.X, a.Y, a.Z);
        }
    }
}
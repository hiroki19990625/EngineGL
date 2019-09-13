using System;
using Newtonsoft.Json;

namespace EngineGL.Mathematics
{
    /// <summary>
    /// 4つの <see cref="float"/> を持つベクトルを表します。
    /// </summary>
    [Serializable]
    public struct Vector4 : IEquatable<Vector4>
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        [JsonIgnore] public float Magnitude => (float) System.Math.Sqrt(SqrMagnitude);

        [JsonIgnore]
        public Vector4 Normalized
        {
            get
            {
                var m = Magnitude;
                return new Vector4(X / m, Y / m, Z / m, W / m);
            }
        }

        [JsonIgnore] public float SqrMagnitude => X * X + Y * Y + Z * Z + W * W;

        public Vector4(float value)
        {
            X = value;
            Y = value;
            Z = value;
            W = value;
        }

        public Vector4(Vector2 vec2)
        {
            X = vec2.X;
            Y = vec2.Y;
            Z = 0f;
            W = 0f;
        }

        public Vector4(float x, float y)
        {
            X = x;
            Y = y;
            Z = 0f;
            W = 0f;
        }

        public Vector4(Vector3 vec3)
        {
            X = vec3.X;
            Y = vec3.Y;
            Z = vec3.Z;
            W = 0f;
        }

        public Vector4(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
            W = 0f;
        }

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vector4(Vector2 vec2a, Vector2 vec2b)
        {
            X = vec2a.X;
            Y = vec2a.Y;
            Z = vec2b.X;
            W = vec2b.Y;
        }

        public float Dot(Vector4 other) => X * other.X + Y * other.Y + Z * other.Z + other.W * W;

        public Vector4 Cross(Vector4 other) =>
            new Vector4(Y * other.Z - Z * other.Y, Z * other.X - X * other.Z, X * other.Y - Y * other.X, W * other.W);

        public bool Equals(Vector4 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector4 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                hashCode = (hashCode * 397) ^ W.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"EngineGL.Structs.Math.Vec4: <X: {X}, Y: {Y}, Z: {Z}, W: {W}>";
        }

        public static Vector4 operator +(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        public static Vector4 operator -(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }

        public static Vector4 operator *(float a, Vector4 b)
        {
            return new Vector4(a * b.X, a * b.Y, a * b.Z, a * b.W);
        }

        public static Vector4 operator *(Vector4 a, float b)
        {
            return new Vector4(a.X * b, a.Y * b, a.Z * b, a.W * b);
        }

        public static Vector4 operator /(float a, Vector4 b)
        {
            return new Vector4(a / b.X, a / b.Y, a / b.Z, a / b.W);
        }

        public static Vector4 operator /(Vector4 a, float b)
        {
            return new Vector4(a.X / b, a.Y / b, a.Z / b, a.W / b);
        }

        public static bool operator ==(Vector4 a, Vector4 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector4 a, Vector4 b)
        {
            return !a.Equals(b);
        }

        public static implicit operator Vector4(OpenTK.Vector4 a)
        {
            return new Vector4(a.X, a.Y, a.Z);
        }

        public static implicit operator Vector4(System.Numerics.Vector4 a)
        {
            return new Vector4(a.X, a.Y, a.Z, a.W);
        }

        public static implicit operator Vector4(Vector3 a)
        {
            return new Vector4(a.X, a.Y, a.Z);
        }

        public static implicit operator OpenTK.Vector4(Vector4 a)
        {
            return new OpenTK.Vector4(a.X, a.Y, a.Z, a.W);
        }

        public static implicit operator System.Numerics.Vector4(Vector4 a)
        {
            return new System.Numerics.Vector4(a.X, a.Y, a.Z, a.W);
        }
    }
}
using System;
using Assimp;
using Newtonsoft.Json;
using OpenTK;

namespace EngineGL.Structs.Math
{
    public struct Vec4 : IEquatable<Vec4>
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        [JsonIgnore] public float Magnitude => (float) System.Math.Sqrt(SqrMagnitude);

        [JsonIgnore]
        public Vec4 Normalized
        {
            get
            {
                var m = Magnitude;
                return new Vec4(X / m, Y / m, Z / m, W / m);
            }
        }

        [JsonIgnore] public float SqrMagnitude => X * X + Y * Y + Z * Z + W * W;

        public Vec4(float value)
        {
            X = value;
            Y = value;
            Z = value;
            W = value;
        }

        public Vec4(Vec2 vec2)
        {
            X = vec2.X;
            Y = vec2.Y;
            Z = 0f;
            W = 0f;
        }

        public Vec4(float x, float y)
        {
            X = x;
            Y = y;
            Z = 0f;
            W = 0f;
        }

        public Vec4(Vec3 vec3)
        {
            X = vec3.X;
            Y = vec3.Y;
            Z = vec3.Z;
            W = 0f;
        }

        public Vec4(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
            W = 0f;
        }

        public Vec4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vec4(Vec2 vec2a, Vec2 vec2b)
        {
            X = vec2a.X;
            Y = vec2a.Y;
            Z = vec2b.X;
            W = vec2b.Y;
        }

        public float Dot(Vec4 other) => X * other.X + Y * other.Y + Z * other.Z + other.W * W;

        public Vec4 Cross(Vec4 other) =>
            new Vec4(Y * other.Z - Z * other.Y, Z * other.X - X * other.Z, X * other.Y - Y * other.X, W * other.W);

        public bool Equals(Vec4 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vec4 other && Equals(other);
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
            return $"EngineGL.Structs.Math.Vec3: <X: {X}, Y: {Y}, Z: {Z}>";
        }

        public static Vec4 operator +(Vec4 a, Vec4 b)
        {
            return new Vec4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        public static Vec4 operator -(Vec4 a, Vec4 b)
        {
            return new Vec4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }

        public static Vec4 operator *(float a, Vec4 b)
        {
            return new Vec4(a * b.X, a * b.Y, a * b.Z, a * b.W);
        }

        public static Vec4 operator *(Vec4 a, float b)
        {
            return new Vec4(a.X * b, a.Y * b, a.Z * b, a.W * b);
        }

        public static Vec4 operator /(float a, Vec4 b)
        {
            return new Vec4(a / b.X, a / b.Y, a / b.Z, a / b.W);
        }

        public static Vec4 operator /(Vec4 a, float b)
        {
            return new Vec4(a.X / b, a.Y / b, a.Z / b, a.W / b);
        }

        public static bool operator ==(Vec4 a, Vec4 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vec4 a, Vec4 b)
        {
            return !a.Equals(b);
        }

        public static implicit operator Vec4(Vector4 a)
        {
            return new Vec4(a.X, a.Y, a.Z);
        }

        public static implicit operator Vec4(System.Numerics.Vector4 a)
        {
            return new Vec4(a.X, a.Y, a.Z, a.W);
        }

        public static implicit operator Vec4(Vec3 a)
        {
            return new Vec4(a.X, a.Y, a.Z);
        }

        public static implicit operator Vector4(Vec4 a)
        {
            return new Vector4(a.X, a.Y, a.Z, a.W);
        }

        public static implicit operator System.Numerics.Vector4(Vec4 a)
        {
            return new System.Numerics.Vector4(a.X, a.Y, a.Z, a.W);
        }
    }
}
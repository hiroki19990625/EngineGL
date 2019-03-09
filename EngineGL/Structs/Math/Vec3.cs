using System;
using Assimp;
using OpenTK;

namespace EngineGL.Structs.Math
{
    [Serializable]
    public struct Vec3 : IEquatable<Vec3>
    {
        public static Vec3 Zero = new Vec3(0f, 0f, 0f);
        public static Vec3 One = new Vec3(1f, 1f, 1f);

        public static Vec3 Up = new Vec3(0f, 1f, 0f);
        public static Vec3 Down = new Vec3(0f, -1f, 0f);
        public static Vec3 Right = new Vec3(1f, 0f, 0f);
        public static Vec3 Left = new Vec3(-1f, 0f, 0f);

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vec3(float value)
        {
            X = value;
            Y = value;
            Z = value;
        }

        public Vec3(Vec2 vec2)
        {
            X = vec2.X;
            Y = vec2.Y;
            Z = 0f;
        }

        public Vec3(float x, float y)
        {
            X = x;
            Y = y;
            Z = 0f;
        }

        public Vec3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public bool Equals(Vec3 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vec3 other && Equals(other);
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

        public static Vec3 operator +(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vec3 operator -(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vec3 operator *(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static Vec3 operator *(float a, Vec3 b)
        {
            return new Vec3(a * b.X, a * b.Y, a * b.Z);
        }

        public static Vec3 operator *(Vec3 a, float b)
        {
            return new Vec3(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vec3 operator /(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        public static Vec3 operator /(float a, Vec3 b)
        {
            return new Vec3(a / b.X, a / b.Y, a / b.Z);
        }

        public static Vec3 operator /(Vec3 a, float b)
        {
            return new Vec3(a.X / b, a.Y / b, a.Z / b);
        }

        public static bool operator ==(Vec3 a, Vec3 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vec3 a, Vec3 b)
        {
            return !a.Equals(b);
        }

        public static implicit operator Vec3(Vector3 a)
        {
            return new Vec3(a.X, a.Y, a.Z);
        }

        public static implicit operator Vec3(System.Numerics.Vector3 a)
        {
            return new Vec3(a.X, a.Y, a.Z);
        }

        public static implicit operator Vec3(Vector3D a)
        {
            return new Vec3(a.X, a.Y, a.Z);
        }

        public static implicit operator Vec3(Vec2 a)
        {
            return new Vec3(a.X, a.Y);
        }

        public static implicit operator Vector3(Vec3 a)
        {
            return new Vector3(a.X, a.Y, a.Z);
        }

        public static implicit operator System.Numerics.Vector3(Vec3 a)
        {
            return new System.Numerics.Vector3(a.X, a.Y, a.Z);
        }

        public static implicit operator Vector3D(Vec3 a)
        {
            return new Vector3D(a.X, a.Y, a.Z);
        }
    }
}
using System;
using System.Runtime.CompilerServices;
using Assimp;
using OpenTK;

namespace EngineGL.Structs
{
    [Serializable]
    public struct Vec2 : IEquatable<Vec2>
    {
        private float X { get; set; }
        private float Y { get; set; }

        public Vec2(float value)
        {
            X = value;
            Y = value;
        }

        public Vec2(float x, float y)
        {
            X = x;
            Y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vec2 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vec2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public static Vec2 operator +(Vec2 a, Vec2 b)
        {
            return new Vec2(a.X + b.X, a.Y + b.Y);
        }

        public static Vec2 operator -(Vec2 a, Vec2 b)
        {
            return new Vec2(a.X - b.X, a.Y - b.Y);
        }

        public static Vec2 operator *(Vec2 a, Vec2 b)
        {
            return new Vec2(a.X * b.X, a.Y * b.Y);
        }

        public static Vec2 operator *(float a, Vec2 b)
        {
            return new Vec2(a * b.X, a * b.Y);
        }

        public static Vec2 operator *(Vec2 a, float b)
        {
            return new Vec2(a.X * b, a.Y * b);
        }

        public static Vec2 operator /(Vec2 a, Vec2 b)
        {
            return new Vec2(a.X / b.X, a.Y / b.Y);
        }

        public static Vec2 operator /(float a, Vec2 b)
        {
            return new Vec2(a / b.X, a / b.Y);
        }

        public static Vec2 operator /(Vec2 a, float b)
        {
            return new Vec2(a.X / b, a.Y / b);
        }

        public static bool operator ==(Vec2 a, Vec2 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vec2 a, Vec2 b)
        {
            return !a.Equals(b);
        }

        public static implicit operator Vec2(Vector2 a)
        {
            return new Vec2(a.X, a.Y);
        }

        public static implicit operator Vec2(System.Numerics.Vector2 a)
        {
            return new Vec2(a.X, a.Y);
        }

        public static implicit operator Vec2(Vector2D a)
        {
            return new Vec2(a.X, a.Y);
        }

        public static implicit operator Vector2(Vec2 a)
        {
            return new Vector2(a.X, a.Y);
        }

        public static implicit operator System.Numerics.Vector2(Vec2 a)
        {
            return new System.Numerics.Vector2(a.X, a.Y);
        }

        public static implicit operator Vector2D(Vec2 a)
        {
            return new Vector2D(a.X, a.Y);
        }
    }
}
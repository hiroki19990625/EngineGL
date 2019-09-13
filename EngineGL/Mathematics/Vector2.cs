using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using Assimp;
using Newtonsoft.Json;

namespace EngineGL.Mathematics
{
    /// <summary>
    /// 2つの <see cref="float"/> を持つベクトルを表します。
    /// </summary>
    [Serializable]
    public struct Vector2 : IEquatable<Vector2>
    {
        public static Vector2 Zero { get; } = new Vector2(0f, 0f);
        public static Vector2 One { get; } = new Vector2(1f, 1f);

        public static Vector2 Up { get; } = new Vector2(0f, 1f);
        public static Vector2 Down { get; } = new Vector2(0f, -1f);
        public static Vector2 Right { get; } = new Vector2(1f, 0f);
        public static Vector2 Left { get; } = new Vector2(-1f, 0f);

        public float X { get; set; }
        public float Y { get; set; }

        [JsonIgnore] public float Magnitude => (float) System.Math.Sqrt(SqrMagnitude);

        [JsonIgnore]
        public Vector2 Normalized
        {
            get
            {
                var m = Magnitude;
                return new Vector2(X / m, Y / m);
            }
        }

        [JsonIgnore] public float SqrMagnitude => X * X + Y * Y;

        public Vector2(float value)
        {
            X = value;
            Y = value;
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2(Vector3 vec3)
        {
            X = vec3.X;
            Y = vec3.Y;
        }

        public float Dot(Vector2 other) => X * other.X + Y * other.Y;

        public float Cross(Vector2 other) => X * other.Y - Y * other.X;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector2 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public override string ToString()
        {
            return $"EngineGL.Structs.Math.Vec2: <X: {X}, Y: {Y}>";
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator *(float a, Vector2 b)
        {
            return new Vector2(a * b.X, a * b.Y);
        }

        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.X * b, a.Y * b);
        }

        public static Vector2 operator /(float a, Vector2 b)
        {
            return new Vector2(a / b.X, a / b.Y);
        }

        public static Vector2 operator /(Vector2 a, float b)
        {
            return new Vector2(a.X / b, a.Y / b);
        }

        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !a.Equals(b);
        }

        public static implicit operator Vector2(OpenTK.Vector2 a)
        {
            return new Vector2(a.X, a.Y);
        }

        public static explicit operator Vector2(OpenTK.Vector3 a)
        {
            return new Vector2(a.X, a.Y);
        }

        public static implicit operator Vector2(System.Numerics.Vector2 a)
        {
            return new Vector2(a.X, a.Y);
        }

        public static explicit operator Vector2(System.Numerics.Vector3 a)
        {
            return new Vector2(a.X, a.Y);
        }

        public static implicit operator Vector2(Vector2D a)
        {
            return new Vector2(a.X, a.Y);
        }

        public static explicit operator Vector2(Vector3D a)
        {
            return new Vector2(a.X, a.Y);
        }

        public static implicit operator Vector2(PointF a)
        {
            return new Vector2(a.X, a.Y);
        }

        public static implicit operator Vector2(SizeF a)
        {
            return new Vector2(a.Width, a.Height);
        }

        public static explicit operator Vector2(Vector3 a)
        {
            return new Vector2(a.X, a.Y);
        }

        public static implicit operator OpenTK.Vector2(Vector2 a)
        {
            return new OpenTK.Vector2(a.X, a.Y);
        }

        public static implicit operator System.Numerics.Vector2(Vector2 a)
        {
            return new System.Numerics.Vector2(a.X, a.Y);
        }

        public static implicit operator Vector2D(Vector2 a)
        {
            return new Vector2D(a.X, a.Y);
        }

        public static implicit operator PointF(Vector2 a)
        {
            return new PointF(a.X, a.Y);
        }

        public static implicit operator SizeF(Vector2 a)
        {
            return new SizeF(a.X, a.Y);
        }
    }
}
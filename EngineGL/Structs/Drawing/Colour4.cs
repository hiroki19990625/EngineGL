using System;
using System.Drawing;

namespace EngineGL.Structs.Drawing
{
    public struct Colour4 : IEquatable<Colour4>
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public Colour4(Colour3 colour3)
        {
            R = colour3.R;
            G = colour3.G;
            B = colour3.B;
            A = 0;
        }

        public Colour4(Color color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
            A = color.A;
        }

        public Colour4(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Colour4(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            A = byte.MaxValue;
        }

        public static implicit operator Colour4(Color color)
        {
            return new Colour4(color.R, color.G, color.B, color.A);
        }

        public static implicit operator Colour4(OpenTK.Graphics.Color4 color)
        {
            return new Colour4(ToByte(color.R), ToByte(color.G), ToByte(color.B), ToByte(color.A));
        }

        public static implicit operator Color(Colour4 color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static implicit operator OpenTK.Graphics.Color4(Colour4 color)
        {
            return new OpenTK.Graphics.Color4(ToFloat(color.R), ToFloat(color.G), ToFloat(color.B), ToFloat(color.A));
        }

        public static implicit operator Colour4(Colour3 a)
        {
            return new Colour4(a.R, a.G, a.B);
        }

        public static bool operator ==(Colour4 a, Colour4 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Colour4 a, Colour4 b)
        {
            return !a.Equals(b);
        }

        public static Colour3 FromRgbInt24(int val)
        {
            return new Colour3((byte) (val >> 16), (byte) (val >> 8), (byte) val);
        }

        public static Colour3 FromBgrInt24(int val)
        {
            return new Colour3((byte) val, (byte) (val >> 8), (byte) (val >> 16));
        }

        public static Colour4 FromRgbaInt32(int val)
        {
            return new Colour4((byte) (val >> 24), (byte) (val >> 16), (byte) (val >> 8), (byte) val);
        }

        public static Colour4 FromArgbInt32(int val)
        {
            return new Colour4((byte) (val >> 16), (byte) (val >> 8), (byte) val, (byte) (val >> 24));
        }

        public static Colour4 FromBgraInt32(int val)
        {
            return new Colour4((byte) val, (byte) (val >> 8), (byte) (val >> 16), (byte) (val >> 24));
        }

        public bool Equals(Colour4 other)
        {
            return R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B) && A.Equals(other.A);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Colour4 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = R.GetHashCode();
                hashCode = (hashCode * 397) ^ G.GetHashCode();
                hashCode = (hashCode * 397) ^ B.GetHashCode();
                hashCode = (hashCode * 397) ^ A.GetHashCode();
                return hashCode;
            }
        }

        public int ToRgbInt24()
        {
            return this.R << 16 | G << 8 | B;
        }

        public int ToBgrInt24()
        {
            return this.B << 16 | G << 8 | R;
        }

        public int ToRgba32()
        {
            return this.R << 24 | this.G << 24 | this.B << 8 | this.A;
        }

        public int ToArgbInt32()
        {
            return this.R << 16 | this.G << 8 | this.B | this.A << 24;
        }

        public int ToBgraInt32()
        {
            return this.B << 24 | this.G << 16 | this.R << 8 | this.A;
        }

        private static byte ToByte(float f)
        {
            return (byte) System.Math.Floor(f * 255);
        }

        private static float ToFloat(byte b)
        {
            return b / 255f;
        }
    }
}
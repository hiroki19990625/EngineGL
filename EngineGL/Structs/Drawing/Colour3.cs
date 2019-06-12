using System;
using System.Drawing;
using OpenTK.Graphics;

namespace EngineGL.Structs.Drawing
{
    public class Colour3 : IEquatable<Colour3>
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public Colour3(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public Colour3(Colour4 colour4)
        {
            R = colour4.R;
            G = colour4.G;
            B = colour4.B;
        }


        public static explicit operator Colour3(Color color)
        {
            return new Colour3(color.R, color.G, color.B);
        }

        public static explicit operator Colour3(OpenTK.Graphics.Color4 color)
        {
            return new Colour3(ToByte(color.R), ToByte(color.G), ToByte(color.B));
        }

        public static implicit operator OpenTK.Graphics.Color4(Colour3 color)
        {
            return new OpenTK.Graphics.Color4(ToFloat(color.R), ToFloat(color.G), ToFloat(color.B), 0f);
        }

        public static implicit operator Color(Colour3 color)
        {
            return Color.FromArgb(0xff, color.R, color.G, color.B);
        }

        public static Colour3 FromRgbInt24(int val)
        {
            return new Colour3((byte) (val >> 16), (byte) (val >> 8), (byte) val);
        }

        public static Colour3 FromBgrInt24(int val)
        {
            return new Colour3((byte) val, (byte) (val >> 8), (byte) (val >> 16));
        }

        public static bool operator ==(Colour3 a, Colour3 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Colour3 a, Colour3 b)
        {
            return !a.Equals(b);
        }

        public bool Equals(Colour3 other)
        {
            return R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Colour3))
                return false;

            return this.Equals((Colour3) obj);
        }

        public int ToRgbInt24()
        {
            return this.R << 16 | G << 8 | B;
        }

        public int ToBgrInt24()
        {
            return this.B << 16 | G << 8 | R;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = R.GetHashCode();
                hashCode = (hashCode * 397) ^ G.GetHashCode();
                hashCode = (hashCode * 397) ^ B.GetHashCode();
                return hashCode;
            }
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
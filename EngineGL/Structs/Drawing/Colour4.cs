using System.Drawing;

namespace EngineGL.Structs.Drawing
{
    public class Colour4
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

        public static implicit operator Colour4(Color color)
        {
            return new Colour4(color.R, color.G, color.B, color.A);
        }

        public static implicit operator Colour4(OpenTK.Graphics.Color4 color)
        {
            return new Colour4(ToByte(color.R), ToByte(color.G), ToByte(color.B), ToByte(color.A));
        }

        public static implicit operator OpenTK.Graphics.Color4(Colour4 color)
        {
            return new OpenTK.Graphics.Color4(ToFloat(color.R), ToFloat(color.G), ToFloat(color.B), ToFloat(color.A));
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
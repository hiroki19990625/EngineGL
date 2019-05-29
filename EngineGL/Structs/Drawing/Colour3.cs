using System.Drawing;

namespace EngineGL.Structs.Drawing
{
    public class Colour3
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


        public static implicit operator Colour3(Color color)
        {
            return new Colour3(color.R, color.G, color.B);
        }

        public static implicit operator Colour3(OpenTK.Graphics.Color4 color)
        {
            return new Colour3(ToByte(color.R), ToByte(color.G), ToByte(color.B));
        }

        public static implicit operator OpenTK.Graphics.Color4(Colour3 color)
        {
            return new OpenTK.Graphics.Color4(ToFloat(color.R), ToFloat(color.G), ToFloat(color.B), 0f);
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
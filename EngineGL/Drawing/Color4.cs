using System;
using System.Drawing;

namespace EngineGL.Drawing
{
    /// <summary>
    /// 3原色の色と透明度(赤・緑・青・アルファ)を表現します。
    /// </summary>
    [Serializable]
    public struct Color4 : IEquatable<Color4>
    {
        /// <summary>
        /// 赤の原色
        /// </summary>
        public byte R { get; set; }

        /// <summary>
        /// 緑の原色
        /// </summary>
        public byte G { get; set; }

        /// <summary>
        /// 青の原色
        /// </summary>
        public byte B { get; set; }


        /// <summary>
        /// 透明度
        /// </summary>
        public byte A { get; set; }

        /// <summary>
        /// 色を指定した値で初期化指定します。
        /// </summary>
        /// <param name="color3"></param>
        public Color4(Color3 color3)
        {
            R = color3.R;
            G = color3.G;
            B = color3.B;
            A = 0;
        }

        /// <summary>
        /// 色を指定した値で初期化指定します。
        /// </summary>
        /// <param name="color"></param>
        public Color4(Color color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
            A = color.A;
        }

        /// <summary>
        /// 色を指定した値で初期化指定します。
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public Color4(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// 色を指定した値で初期化指定します。
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public Color4(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            A = byte.MaxValue;
        }

        public static implicit operator Color4(Color color)
        {
            return new Color4(color.R, color.G, color.B, color.A);
        }

        public static implicit operator Color4(OpenTK.Graphics.Color4 color)
        {
            return new Color4(ToByte(color.R), ToByte(color.G), ToByte(color.B), ToByte(color.A));
        }

        public static implicit operator Color(Color4 color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static implicit operator OpenTK.Graphics.Color4(Color4 color)
        {
            return new OpenTK.Graphics.Color4(ToFloat(color.R), ToFloat(color.G), ToFloat(color.B), ToFloat(color.A));
        }

        public static implicit operator Color4(Color3 a)
        {
            return new Color4(a.R, a.G, a.B);
        }

        public static bool operator ==(Color4 a, Color4 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Color4 a, Color4 b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// <see cref="int"/> から <see cref="Color3"/> へRGB形式で変換します。
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Color3 FromRgbInt24(int val)
        {
            return new Color3((byte) (val >> 16), (byte) (val >> 8), (byte) val);
        }

        /// <summary>
        /// <see cref="int"/> から <see cref="Color3"/> へBGR形式で変換します。
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Color3 FromBgrInt24(int val)
        {
            return new Color3((byte) val, (byte) (val >> 8), (byte) (val >> 16));
        }

        /// <summary>
        /// <see cref="int"/> から <see cref="Color3"/> へRGBA形式で変換します。
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Color4 FromRgbaInt32(int val)
        {
            return new Color4((byte) (val >> 24), (byte) (val >> 16), (byte) (val >> 8), (byte) val);
        }

        /// <summary>
        /// <see cref="int"/> から <see cref="Color3"/> へARGB形式で変換します。
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Color4 FromArgbInt32(int val)
        {
            return new Color4((byte) (val >> 16), (byte) (val >> 8), (byte) val, (byte) (val >> 24));
        }

        /// <summary>
        /// <see cref="int"/> から <see cref="Color3"/> へBRGA形式で変換します。
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Color4 FromBgraInt32(int val)
        {
            return new Color4((byte) val, (byte) (val >> 8), (byte) (val >> 16), (byte) (val >> 24));
        }

        public bool Equals(Color4 other)
        {
            return R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B) && A.Equals(other.A);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Color4 other && Equals(other);
        }

        public override string ToString()
        {
            return $"EngineGL.Structs.Drawing.Colour4: <R: {R}, G: {G}, B: {B}, A: {A}>";
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

        /// <summary>
        /// <see cref="Color4"/> から <see cref="int"/> へRGB形式で変換します。
        /// </summary>
        /// <returns></returns>
        public int ToRgbInt24()
        {
            return this.R << 16 | G << 8 | B;
        }

        /// <summary>
        /// <see cref="Color4"/> から <see cref="int"/> へBGR形式で変換します。
        /// </summary>
        /// <returns></returns>
        public int ToBgrInt24()
        {
            return this.B << 16 | G << 8 | R;
        }

        /// <summary>
        /// <see cref="Color4"/> から <see cref="int"/> へRGBA形式で変換します。
        /// </summary>
        /// <returns></returns>
        public int ToRgba32()
        {
            return this.R << 24 | this.G << 24 | this.B << 8 | this.A;
        }

        /// <summary>
        /// <see cref="Color4"/> から <see cref="int"/> へARGB形式で変換します。
        /// </summary>
        /// <returns></returns>
        public int ToArgbInt32()
        {
            return this.R << 16 | this.G << 8 | this.B | this.A << 24;
        }

        /// <summary>
        /// <see cref="Color4"/> から <see cref="int"/> へBGRA形式で変換します。
        /// </summary>
        /// <returns></returns>
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
using System;
using System.Drawing;
using OpenTK.Graphics;

namespace EngineGL.Structs.Drawing
{
    /// <summary>
    /// 3原色の色(赤・緑・青)を表現します。
    /// </summary>
    [Serializable]
    public struct Colour3 : IEquatable<Colour3>
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
        /// 色を指定した値で初期化します。
        /// </summary>
        /// <param name="r">赤の原色</param>
        /// <param name="g">緑の原色</param>
        /// <param name="b">青の原色</param>
        public Colour3(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// 色を指定した値で初期化指定します。
        /// </summary>
        /// <param name="colour4"><see cref="Colour4"/> の透明度を無視した値が使用されます。</param>
        public Colour3(Colour4 colour4)
        {
            R = colour4.R;
            G = colour4.G;
            B = colour4.B;
        }

        /// <summary>
        /// <see cref="Color"/> から明示的にキャストします。
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static explicit operator Colour3(Color color)
        {
            return new Colour3(color.R, color.G, color.B);
        }

        /// <summary>
        /// <see cref="Color4"/> から明示的にキャストします。
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static explicit operator Colour3(Color4 color)
        {
            return new Colour3(ToByte(color.R), ToByte(color.G), ToByte(color.B));
        }

        /// <summary>
        /// <see cref="Color4"/> へ暗黙的にキャストします。
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static implicit operator Color4(Colour3 color)
        {
            return new Color4(ToFloat(color.R), ToFloat(color.G), ToFloat(color.B), 0f);
        }

        /// <summary>
        /// <see cref="Color"/> へ暗黙的にキャストします。
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static implicit operator Color(Colour3 color)
        {
            return Color.FromArgb(0xff, color.R, color.G, color.B);
        }

        /// <summary>
        /// <see cref="Colour4"/> へ明示的にキャストします。
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static explicit operator Colour3(Colour4 color)
        {
            return new Colour3(color);
        }

        /// <summary>
        /// <see cref="int"/> から <see cref="Colour3"/> へRGB形式で変換します。
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Colour3 FromRgbInt24(int val)
        {
            return new Colour3((byte) (val >> 16), (byte) (val >> 8), (byte) val);
        }

        /// <summary>
        /// <see cref="int"/> から <see cref="Colour3"/> へBGR形式で変換します。
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
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
            if (ReferenceEquals(null, obj)) return false;
            return obj is Colour3 other && Equals(other);
        }

        public override string ToString()
        {
            return $"EngineGL.Structs.Drawing.Colour3: <R: {R}, G: {G}, B: {B}>";
        }

        /// <summary>
        /// <see cref="Colour3"/> から <see cref="int"/> へRGB形式で変換します。
        /// </summary>
        /// <returns></returns>
        public int ToRgbInt24()
        {
            return this.R << 16 | G << 8 | B;
        }

        /// <summary>
        /// <see cref="Colour3"/> から <see cref="int"/> へBGR形式で変換します。
        /// </summary>
        /// <returns></returns>
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
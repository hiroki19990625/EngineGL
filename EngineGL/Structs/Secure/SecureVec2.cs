using System;
using System.Linq;
using EngineGL.Structs.Math;

namespace EngineGL.Structs.Secure
{
    public class SecureVec2 : SecureValue<Vec2>
    {
        private const int INT_SIZE = 4;
        private const int INT_COUNT = 2;

        public SecureVec2(Vec2 value) : base(value)
        {
        }

        protected override byte[] ToSecure(Vec2 value)
        {
            byte[] buffer = new byte[INT_SIZE * INT_COUNT];
            Array.Copy(BitConverter.GetBytes(value.X), buffer, INT_SIZE);
            Array.Copy(BitConverter.GetBytes(value.Y), 0, buffer, INT_SIZE, INT_SIZE);

            return buffer;
        }

        protected override Vec2 FromSecure(byte[] secure)
        {
            float x = BitConverter.ToSingle(secure, 0);
            float y = BitConverter.ToSingle(secure, INT_SIZE);

            return new Vec2(x, y);
        }

        public static implicit operator Vec2(SecureVec2 a)
        {
            return a.Value;
        }
    }
}
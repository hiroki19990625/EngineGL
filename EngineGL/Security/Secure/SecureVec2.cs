using System;
using EngineGL.Mathematics;

namespace EngineGL.Security.Secure
{
    public class SecureVec2 : SecureValue<Vector2>
    {
        private const int INT_SIZE = 4;
        private const int INT_COUNT = 2;

        public SecureVec2(Vector2 value) : base(value)
        {
        }

        protected override byte[] ToSecure(Vector2 value)
        {
            byte[] buffer = new byte[INT_SIZE * INT_COUNT];
            Array.Copy(BitConverter.GetBytes(value.X), buffer, INT_SIZE);
            Array.Copy(BitConverter.GetBytes(value.Y), 0, buffer, INT_SIZE, INT_SIZE);

            return buffer;
        }

        protected override Vector2 FromSecure(byte[] secure)
        {
            float x = BitConverter.ToSingle(secure, 0);
            float y = BitConverter.ToSingle(secure, INT_SIZE);

            return new Vector2(x, y);
        }

        public static implicit operator Vector2(SecureVec2 a)
        {
            return a.Value;
        }
    }
}
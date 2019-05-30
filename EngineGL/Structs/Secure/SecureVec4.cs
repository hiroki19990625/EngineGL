using System;
using EngineGL.Structs.Math;

namespace EngineGL.Structs.Secure
{
    public class SecureVec4 : SecureValue<Vec4>
    {
        private const int INT_SIZE = 4;
        private const int INT_COUNT = 3;

        public SecureVec4(Vec4 value) : base(value)
        {
        }

        protected override byte[] ToSecure(Vec4 value)
        {
            byte[] buffer = new byte[INT_SIZE * INT_COUNT];
            Array.Copy(BitConverter.GetBytes(value.X), buffer, INT_SIZE);
            Array.Copy(BitConverter.GetBytes(value.Y), 0, buffer, INT_SIZE, INT_SIZE);
            Array.Copy(BitConverter.GetBytes(value.Z), 0, buffer, INT_SIZE * 2, INT_SIZE);
            Array.Copy(BitConverter.GetBytes(value.W), 0, buffer, INT_SIZE * 3, INT_SIZE);

            return buffer;
        }

        protected override Vec4 FromSecure(byte[] secure)
        {
            float x = BitConverter.ToSingle(secure, 0);
            float y = BitConverter.ToSingle(secure, INT_SIZE);
            float z = BitConverter.ToSingle(secure, INT_SIZE * 2);
            float w = BitConverter.ToSingle(secure, INT_SIZE * 3);

            return new Vec4(x, y, z, w);
        }
    }
}
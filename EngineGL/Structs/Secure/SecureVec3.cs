using System;
using EngineGL.Structs.Math;

namespace EngineGL.Structs.Secure
{
    public class SecureVec3 : SecureValue<Vec3>
    {
        private const int INT_SIZE = 4;
        private const int INT_COUNT = 3;

        public SecureVec3(Vec3 value) : base(value)
        {
        }

        protected override byte[] ToSecure(Vec3 value)
        {
            byte[] buffer = new byte[INT_SIZE * INT_COUNT];
            Array.Copy(BitConverter.GetBytes(value.X), buffer, INT_SIZE);
            Array.Copy(BitConverter.GetBytes(value.Y), 0, buffer, INT_SIZE, INT_SIZE);
            Array.Copy(BitConverter.GetBytes(value.Z), 0, buffer, INT_SIZE * 2, INT_SIZE);

            return buffer;
        }

        protected override Vec3 FromSecure(byte[] secure)
        {
            float x = BitConverter.ToSingle(secure, 0);
            float y = BitConverter.ToSingle(secure, INT_SIZE);
            float z = BitConverter.ToSingle(secure, INT_SIZE * 2);

            return new Vec3(x, y, z);
        }

        public static implicit operator Vec3(SecureVec3 a)
        {
            return a.Value;
        }
    }
}
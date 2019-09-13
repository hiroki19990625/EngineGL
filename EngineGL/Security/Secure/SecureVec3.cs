using System;
using EngineGL.Mathematics;

namespace EngineGL.Security.Secure
{
    public class SecureVec3 : SecureValue<Vector3>
    {
        private const int INT_SIZE = 4;
        private const int INT_COUNT = 3;

        public SecureVec3(Vector3 value) : base(value)
        {
        }

        protected override byte[] ToSecure(Vector3 value)
        {
            byte[] buffer = new byte[INT_SIZE * INT_COUNT];
            Array.Copy(BitConverter.GetBytes(value.X), buffer, INT_SIZE);
            Array.Copy(BitConverter.GetBytes(value.Y), 0, buffer, INT_SIZE, INT_SIZE);
            Array.Copy(BitConverter.GetBytes(value.Z), 0, buffer, INT_SIZE * 2, INT_SIZE);

            return buffer;
        }

        protected override Vector3 FromSecure(byte[] secure)
        {
            float x = BitConverter.ToSingle(secure, 0);
            float y = BitConverter.ToSingle(secure, INT_SIZE);
            float z = BitConverter.ToSingle(secure, INT_SIZE * 2);

            return new Vector3(x, y, z);
        }

        public static implicit operator Vector3(SecureVec3 a)
        {
            return a.Value;
        }
    }
}
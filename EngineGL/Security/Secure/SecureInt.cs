using System;

namespace EngineGL.Security.Secure
{
    public class SecureInt : SecureValue<int>
    {
        public SecureInt(int value) : base(value)
        {
        }

        protected override byte[] ToSecure(int value)
        {
            return BitConverter.GetBytes(value);
        }

        protected override int FromSecure(byte[] secure)
        {
            return BitConverter.ToInt32(secure, 0);
        }

        public static implicit operator int(SecureInt a)
        {
            return a.Value;
        }
    }
}
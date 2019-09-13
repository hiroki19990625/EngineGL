using System;

namespace EngineGL.Security.Secure
{
    public class SecureLong : SecureValue<long>
    {
        public SecureLong(long value) : base(value)
        {
        }

        protected override byte[] ToSecure(long value)
        {
            return BitConverter.GetBytes(value);
        }

        protected override long FromSecure(byte[] secure)
        {
            return BitConverter.ToInt64(secure, 0);
        }

        public static implicit operator long(SecureLong a)
        {
            return a.Value;
        }
    }
}
using System;

namespace EngineGL.Structs.Secure
{
    public class SecureLong : SecureValue<long>
    {
        public SecureLong(long value) : base(value)
        {
        }

        public override byte[] ToSecure(long value)
        {
            return BitConverter.GetBytes(value);
        }

        public override long FromSecure(byte[] secure)
        {
            return BitConverter.ToInt64(secure, 0);
        }

        public static implicit operator long(SecureLong a)
        {
            return a.Value;
        }
    }
}
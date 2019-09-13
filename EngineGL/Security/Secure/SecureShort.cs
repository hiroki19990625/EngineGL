using System;

namespace EngineGL.Security.Secure
{
    public class SecureShort : SecureValue<short>
    {
        public SecureShort(short value) : base(value)
        {
        }

        protected override byte[] ToSecure(short value)
        {
            return BitConverter.GetBytes(value);
        }

        protected override short FromSecure(byte[] secure)
        {
            return BitConverter.ToInt16(secure, 0);
        }

        public static implicit operator short(SecureShort a)
        {
            return a.Value;
        }
    }
}
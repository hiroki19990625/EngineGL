using System;

namespace EngineGL.Structs.Secure
{
    public class SecureShort : SecureValue<short>
    {
        public SecureShort(short value) : base(value)
        {
        }

        public override byte[] ToSecure(short value)
        {
            return BitConverter.GetBytes(value);
        }

        public override short FromSecure(byte[] secure)
        {
            return BitConverter.ToInt16(secure, 0);
        }

        public static implicit operator short(SecureShort a)
        {
            return a.Value;
        }
    }
}
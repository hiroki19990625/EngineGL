using System;

namespace EngineGL.Structs.Secure
{
    public class SecureDouble : SecureValue<double>
    {
        public SecureDouble(double value) : base(value)
        {
        }

        public override byte[] ToSecure(double value)
        {
            return BitConverter.GetBytes(value);
        }

        public override double FromSecure(byte[] secure)
        {
            return BitConverter.ToDouble(secure, 0);
        }

        public static implicit operator double(SecureDouble a)
        {
            return a.Value;
        }
    }
}
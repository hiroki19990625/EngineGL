using System;

namespace EngineGL.Security.Secure
{
    public class SecureFloat : SecureValue<float>
    {
        public SecureFloat(float value) : base(value)
        {
        }

        protected override byte[] ToSecure(float value)
        {
            return BitConverter.GetBytes(value);
        }

        protected override float FromSecure(byte[] secure)
        {
            return BitConverter.ToSingle(secure, 0);
        }

        public static implicit operator float(SecureFloat a)
        {
            return a.Value;
        }
    }
}
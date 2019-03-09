using System;

namespace EngineGL.Structs.Secure
{
    public class SecureFloat : SecureValue<float>
    {
        public SecureFloat(float value) : base(value)
        {
        }

        public override byte[] ToSecure(float value)
        {
            return BitConverter.GetBytes(value);
        }

        public override float FromSecure(byte[] secure)
        {
            return BitConverter.ToSingle(secure, 0);
        }

        public static implicit operator float(SecureFloat a)
        {
            return a.Value;
        }
    }
}
using System;
using System.Text;

namespace EngineGL.Structs.Secure
{
    public class SecureString : SecureValue<string>
    {
        public SecureString(string value) : base(value)
        {
        }

        public override byte[] ToSecure(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        public override string FromSecure(byte[] secure)
        {
            return Encoding.UTF8.GetString(secure);
        }

        public static implicit operator string(SecureString a)
        {
            return a.Value;
        }
    }
}
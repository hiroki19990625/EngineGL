using System.Text;

namespace EngineGL.Security.Secure
{
    public class SecureString : SecureValue<string>
    {
        public SecureString(string value) : base(value)
        {
        }

        protected override byte[] ToSecure(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        protected override string FromSecure(byte[] secure)
        {
            return Encoding.UTF8.GetString(secure);
        }

        public static implicit operator string(SecureString a)
        {
            return a.Value;
        }
    }
}
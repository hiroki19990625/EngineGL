using System.Security.Cryptography;

namespace EngineGL.Utils
{
    public class Sha256
    {
        public byte[] CreateHash(byte[] buffer)
        {
            using (SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider())
            {
                return sha256.ComputeHash(buffer);
            }
        }
    }
}
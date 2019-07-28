using System.Security.Cryptography;

namespace EngineGL.Utils
{
    public class Dsa
    {
        public (string, string) CreateKey()
        {
            string privateKey;
            string publicKey;
            using (DSACryptoServiceProvider rsa = new DSACryptoServiceProvider())
            {
                publicKey = rsa.ToXmlString(false);
                privateKey = rsa.ToXmlString(true);
            }

            return (publicKey, privateKey);
        }

        public bool Verify(byte[] data, byte[] signature, string publickey)
        {
            using (DSACryptoServiceProvider dsa = new DSACryptoServiceProvider())
            {
                dsa.FromXmlString(publickey);

                return dsa.VerifyData(data, signature, HashAlgorithmName.SHA1);
            }
        }

        public byte[] Sign(byte[] data, string privatekey)
        {
            byte[] sign;
            using (DSACryptoServiceProvider dsa = new DSACryptoServiceProvider())
            {
                dsa.FromXmlString(privatekey);

                return dsa.SignData(data, HashAlgorithmName.SHA1);
            }
        }
    }
}
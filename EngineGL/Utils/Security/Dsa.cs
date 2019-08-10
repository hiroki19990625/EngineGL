using System.Security.Cryptography;

namespace EngineGL.Utils.Security
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

        public bool Verify(byte[] data, byte[] signature, string publicKey)
        {
            using (DSACryptoServiceProvider dsa = new DSACryptoServiceProvider())
            {
                dsa.FromXmlString(publicKey);

                return dsa.VerifyData(data, signature, HashAlgorithmName.SHA1);
            }
        }

        public bool Verify(byte[] data, byte[] signature, string publicKey, HashAlgorithmName alg)
        {
            using (DSACryptoServiceProvider dsa = new DSACryptoServiceProvider())
            {
                dsa.FromXmlString(publicKey);

                return dsa.VerifyData(data, signature, alg);
            }
        }

        public byte[] Sign(byte[] data, string privateKey)
        {
            using (DSACryptoServiceProvider dsa = new DSACryptoServiceProvider())
            {
                dsa.FromXmlString(privateKey);

                return dsa.SignData(data, HashAlgorithmName.SHA1);
            }
        }

        public byte[] Sign(byte[] data, string privateKey, HashAlgorithmName alg)
        {
            using (DSACryptoServiceProvider dsa = new DSACryptoServiceProvider())
            {
                dsa.FromXmlString(privateKey);

                return dsa.SignData(data, alg);
            }
        }
    }
}
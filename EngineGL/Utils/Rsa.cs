using System.Security.Cryptography;

namespace EngineGL.Utils
{
    public class Rsa
    {
        public (string, string) CreateKey()
        {
            string privateKey;
            string publicKey;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                publicKey = rsa.ToXmlString(false);
                privateKey = rsa.ToXmlString(true);
            }

            return (publicKey, privateKey);
        }

        public byte[] Encrypt(byte[] data, string publickey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publickey);

                data = rsa.Encrypt(data, false);

                return data;
            }
        }

        public byte[] Decrypt(byte[] data, string privatekey)
        {
            byte[] decrypted;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privatekey);

                decrypted = rsa.Decrypt(data, false);

                return decrypted;
            }
        }
        
        public bool Verify(byte[] data, byte[] signature, string publickey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publickey);

                return rsa.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }

        public byte[] Sign(byte[] data, string privatekey)
        {
            byte[] sign;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privatekey);

                return rsa.SignData(data, HashAlgorithmName.SHA256);
            }
        }
    }
}
using System.Security.Cryptography;

namespace EngineGL.Utils.Security
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

        public bool Verify(byte[] data, byte[] signature, string publicKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);

                return rsa.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }

        public bool Verify(byte[] data, byte[] signature, string publicKey, HashAlgorithmName alg)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);

                return rsa.VerifyData(data, signature, alg, RSASignaturePadding.Pkcs1);
            }
        }

        public bool Verify(byte[] data, byte[] signature, string publicKey, HashAlgorithmName alg,
            RSASignaturePadding signaturePadding)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);

                return rsa.VerifyData(data, signature, alg, signaturePadding);
            }
        }

        public byte[] Sign(byte[] data, string privateKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);

                return rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }

        public byte[] Sign(byte[] data, string privateKey, HashAlgorithmName alg)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);

                return rsa.SignData(data, alg, RSASignaturePadding.Pkcs1);
            }
        }

        public byte[] Sign(byte[] data, string privateKey, HashAlgorithmName alg, RSASignaturePadding signaturePadding)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);

                return rsa.SignData(data, alg, signaturePadding);
            }
        }
    }
}
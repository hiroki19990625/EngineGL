using System;
using System.Security.Cryptography;
using System.Text;

namespace EngineGL.Utils
{
    public class Rsa
    {
        public (string,string) CreateKey()
        { 
            string privateKey;
            string publicKey;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                publicKey = rsa.ToXmlString(false);
                privateKey = rsa.ToXmlString(true);
            }

            return (publicKey,privateKey);
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
    }
}
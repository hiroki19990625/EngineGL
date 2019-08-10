using System.Security.Cryptography;

namespace EngineGL.Utils.Security
{
    public class Aes
    {
        private ICryptoTransform _encryptor;
        private ICryptoTransform _decryptor;

        public byte[] IV { get; set; }
        public byte[] Key { get; set; }

        public void Init()
        {
            using (AesManaged aes = new AesManaged())
            {
                aes.KeySize = Key.Length * 8;
                aes.BlockSize = 128;

                aes.Mode = CipherMode.CBC;

                aes.IV = IV;
                aes.Key = Key;

                aes.Padding = PaddingMode.PKCS7;

                _encryptor = aes.CreateEncryptor();
                _decryptor = aes.CreateDecryptor();
            }
        }

        public byte[] Encrypt(byte[] data)
        {
            return _encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Decrypt(byte[] data)
        {
            return _decryptor.TransformFinalBlock(data, 0, data.Length);
        }
    }
}
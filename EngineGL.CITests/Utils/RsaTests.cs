using System.Text;
using EngineGL.Utils;
using NUnit.Framework;

namespace EngineGL.CITests.Utils
{
    [TestFixture]
    public class RsaTests
    {
        [Test]
        public void RsaTest()
        {
            string publicKey;
            string privateKey;
            string testData;
            string decrypted;
            byte[] encrypted;
            byte[] bytes;
            byte[] sign;

            testData = "Hello World!";
            Rsa rsa = new Rsa();
            (publicKey, privateKey) = rsa.CreateKey();

            /*bytes = Encoding.UTF8.GetBytes(testData);
            sign = rsa.Sign(bytes, privateKey);
            if (sign.Equals(testData))
            {
                Assert.Fail("署名に失敗");
            }

            if (!rsa.Verify(bytes, sign, publicKey))
            {
                Assert.Fail("検証に失敗");
            }*/

            encrypted = rsa.Encrypt(Encoding.UTF8.GetBytes(testData), publicKey);
            if (encrypted.Equals(testData))
            {
                Assert.Fail("暗号化に失敗");
            }

            decrypted = Encoding.UTF8.GetString(rsa.Decrypt(encrypted, privateKey));
            if (testData != decrypted)
            {
                Assert.Fail("複合に失敗");
            }
        }
    }
}
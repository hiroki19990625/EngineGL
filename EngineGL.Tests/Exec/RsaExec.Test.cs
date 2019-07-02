using System;
using System.Diagnostics;
using System.Text;
using EngineGL.Utils;
using NUnit.Framework;

namespace EngineGL.Tests.Exec
{
    [TestFixture]
    public class RsaExec_Test
    {
        [Test]
        public void RsaTest()
        {
            string publicKey;
            string privateKey;
            string testData;
            string decrypted;
            byte[] encrypted;

            testData = "Hello World!";
            Rsa rsa = new Rsa();
            (publicKey, privateKey) = rsa.CreateKey();

            encrypted = rsa.Encrypt(Encoding.UTF8.GetBytes(testData), publicKey);
            if (encrypted.Equals(testData))
            {
                Assert.Fail("暗号化に失敗");
            }

            decrypted = Encoding.UTF8.GetString(rsa.Decrypt(encrypted, privateKey));
            if (testData != (decrypted))
            {
                Assert.Fail("複合に失敗");
            }
        }
    }
}
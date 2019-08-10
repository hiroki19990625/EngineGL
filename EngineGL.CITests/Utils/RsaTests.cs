using System.Text;
using EngineGL.Utils;
using EngineGL.Utils.Security;
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

            testData = "Hello World! ppppppppppppppppppppppppppppp" +
                       "pppppppppppppppppppppppppppppppppppppppppp" +
                       "pppppppppppppppppppppppppppppppppppppppppp" +
                       "pppppppppppppppppppppppppppppppppppppppppp" +
                       "pppppppppppppppppppppppppppppppppppppppppp" +
                       "pppppppppppppppppppppppppppppppppppppppppp" +
                       "pppppppppppppppppppppppppppppppppppppppppp" +
                       "pppppppppppppppppppppppppppppppppppppppppp" +
                       "pppppppppppppppppppppppppppppppppppppppppp" +
                       "pppppppppppppppppppppppppppppppppppppppppp" +
                       "pppppppppppppppppppppppppppppppppppppppppp" +
                       "pppppppppppppppppppppp";
            Rsa rsa = new Rsa();
            (publicKey, privateKey) = rsa.CreateKey();

            bytes = Encoding.UTF8.GetBytes(testData);
            sign = rsa.Sign(bytes, privateKey);
            if (sign.Equals(testData))
            {
                Assert.Fail("署名に失敗");
            }

            if (!rsa.Verify(bytes, sign, publicKey))
            {
                Assert.Fail("検証に失敗");
            }

            bytes[0] = 100;
            bytes[1] = 200;
            if (rsa.Verify(bytes, sign, publicKey))
            {
                Assert.Fail("改ざんされているのに検証に成功");
            }
        }
    }
}
using System.Text;
using EngineGL.Utils;
using NUnit.Framework;

namespace EngineGL.CITests.Utils
{
    [TestFixture]
    public class DsaTests
    {
        [Test]
        public void DsaTest()
        {
            string publicKey;
            string privateKey;
            string testData;
            byte[] bytes;
            byte[] sign;

            testData = "Hello World!";
            Dsa dsa = new Dsa();
            (publicKey, privateKey) = dsa.CreateKey();

            bytes = Encoding.UTF8.GetBytes(testData);

            sign = dsa.Sign(bytes, privateKey);
            if (sign.Equals(testData))
            {
                Assert.Fail("署名に失敗");
            }

            if (!dsa.Verify(bytes, sign, publicKey))
            {
                Assert.Fail("検証に失敗");
            }
        }
    }
}
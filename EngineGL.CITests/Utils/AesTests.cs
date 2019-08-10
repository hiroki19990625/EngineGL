using System.Text;
using EngineGL.Utils.Security;
using NUnit.Framework;

namespace EngineGL.CITests.Utils
{
    [TestFixture]
    public class AesTests
    {
        [Test]
        public void AesTest()
        {
            Aes aes = new Aes();
            aes.Key = Encoding.UTF8.GetBytes("1234567890abcdef1234567890abcdef");
            aes.IV = Encoding.UTF8.GetBytes("1234567890abcdef");

            string sd = "Hello World! ppppppppppppppppppppppppppppp" +
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
            byte[] data = Encoding.UTF8.GetBytes(sd);

            aes.Init();

            byte[] e = aes.Encrypt(data);

            Assert.True(Encoding.UTF8.GetString(aes.Decrypt(e)) == sd);
        }
    }
}
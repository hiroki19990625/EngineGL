using EngineGL.Mathematics;
using EngineGL.Security.Secure;
using NUnit.Framework;

namespace EngineGL.CITests.Security.Secure
{
    [TestFixture]
    public class SecureTests
    {
        [Test]
        public void Tests()
        {
            SecureShort secureShort = new SecureShort(500);
            SecureInt secureInt = new SecureInt(100000);
            SecureLong secureLong = new SecureLong(10000000000);
            SecureFloat secureFloat = new SecureFloat(123.456789f);
            SecureDouble secureDouble = new SecureDouble(123.456789d);
            SecureString secureString = new SecureString("secure");
            Assert.IsTrue(secureShort == 500);
            Assert.IsTrue(secureInt == 100000);
            Assert.IsTrue(secureLong == 10000000000);
            Assert.IsTrue(secureFloat == 123.456789f);
            Assert.IsTrue(secureDouble == 123.456789d);
            Assert.IsTrue(secureString == "secure");
            Assert.IsTrue(new SecureVec2(new Vector2(10f, 10f)) == new Vector2(10f, 10f));
            Assert.IsTrue(new SecureVec3(new Vector3(10f, 10f, 10f)) == new Vector3(10f, 10f, 10f));
            Assert.IsTrue(new SecureVec4(new Vector4(10f, 10f, 10f, 10f)) == new Vector4(10f, 10f, 10f, 10f));

            secureInt.Set(123);
            secureString.Set("network");
            Assert.IsTrue(secureInt == 123);
            Assert.IsTrue(secureString == "network");
        }
    }
}
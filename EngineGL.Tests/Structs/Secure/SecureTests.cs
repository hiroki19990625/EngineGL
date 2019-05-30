using System;
using EngineGL.Structs.Secure;
using NUnit.Framework;

namespace EngineGL.Tests.Structs.Secure
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
            Assert.AreEqual((short) secureShort, 500);
            Assert.AreEqual((int) secureInt, 100000);
            Assert.AreEqual((long) secureLong, 10000000000);
            Assert.AreEqual((float) secureFloat, 123.456789f);
            Assert.AreEqual((double) secureDouble, 123.456789d);
            Assert.AreEqual((string) secureString, "secure");

            secureInt.Set(500);
            secureString.Set("network");
            Assert.AreEqual((int) secureInt, 500);
            Assert.AreEqual((string) secureString, "network");
        }
    }
}
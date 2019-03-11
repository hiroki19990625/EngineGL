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
            Console.WriteLine(secureShort);
            Console.WriteLine(secureInt);
            Console.WriteLine(secureLong);
            Console.WriteLine(secureFloat);
            Console.WriteLine(secureDouble);
            Console.WriteLine(secureString);

            secureInt.Set(500);
            secureString.Set("network");
            Console.WriteLine(secureInt);
            Console.WriteLine(secureString);
        }
    }
}
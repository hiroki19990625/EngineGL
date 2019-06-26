using EngineGL.Structs.Drawing;
using NUnit.Framework;

namespace EngineGL.CITests.Structs.Drawing
{
    public class ColourTests
    {
        [TestCase(0, 1, 2, 0)]
        [TestCase(255, 255, 255, 255)]
        [TestCase(157, 58, 47, 18)]
        public void ColourEqualTests(byte r, byte g, byte b, byte a)
        {
            Assert.AreEqual(new Colour3(r, g, b), new Colour3(r, g, b));
            Assert.AreEqual(new Colour4(r, g, b, a), new Colour4(r, g, b, a));
            Assert.IsTrue(new Colour3(r, g, b) == new Colour4(r, g, b, 255));
        }
    }
}
using EngineGL.Drawing;
using NUnit.Framework;

namespace EngineGL.CITests.Drawing
{
    public class ColorTests
    {
        [TestCase(0, 1, 2, 0)]
        [TestCase(255, 255, 255, 255)]
        [TestCase(157, 58, 47, 18)]
        public void ColourEqualTests(byte r, byte g, byte b, byte a)
        {
            Assert.AreEqual(new Color3(r, g, b), new Color3(r, g, b));
            Assert.AreEqual(new Color4(r, g, b, a), new Color4(r, g, b, a));
            Assert.IsTrue(new Color3(r, g, b) == new Color4(r, g, b, 255));
        }
    }
}
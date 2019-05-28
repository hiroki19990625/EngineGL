

using EngineGL.Structs.Math;
using NUnit.Framework;

namespace EngineGL.Tests.Structs.Math
{
    [TestFixture]
    public class VecTests
    {
        [TestCase(0, 1, 2)]
        [TestCase(325325, 34346463, 87654)]
        [TestCase(3.333f, 34.444f, 33.5436f)]
        public void VecEqualTests(float x, float y, float z)
        {
            Assert.AreEqual(new Vec2(x, y), new Vec2(x, y));
            Assert.AreEqual(new Vec3(x, y, z), new Vec3(x, y, z));
            Assert.IsTrue(new Vec2(x, y) == new Vec3(x, y, 0));
        }

        [Test]
        public void VecMethodTests()
        {
            Assert.AreEqual(new Vec2(2, 5).SqrMagnitude, 2 * 2 + 5 * 5);
            Assert.AreEqual(new Vec2(0, 5).Magnitude, 5);
            Assert.AreEqual(new Vec3(2, 5, 3).SqrMagnitude, 2 * 2 + 5 * 5 + 3 * 3);
            Assert.AreEqual(new Vec3(0, 5, 0).Magnitude, 5);

            Assert.AreEqual(new Vec2(1, 8).Dot(new Vec2(9, 6)), 1 * 9 + 8 * 6);
            Assert.AreEqual(new Vec3(1, 8, 2).Dot(new Vec3(9, 6, 5)), 1 * 9 + 8 * 6 + 2 * 5);

            Assert.AreEqual(new Vec2(1, 8).Cross(new Vec2(9, 6)), 1 * 6 - 8 * 9);
            Assert.AreEqual(new Vec3(1, 8, 2).Cross(new Vec3(9, 6, 5)), new Vec3(8 * 5 - 2 * 6, 2 * 9 - 1 * 5, 1 * 6 - 8 * 9));
        }

    }
}

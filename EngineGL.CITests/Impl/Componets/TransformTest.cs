using EngineGL.Impl;
using EngineGL.Structs.Math;
using NUnit.Framework;

namespace EngineGL.CITests.Impl.Componets
{
    [TestFixture]
    public class TransformTest
    {
        [Test]
        public void TransformTest1()
        {
            var obj = new GameObject();
            Assert.AreEqual(obj.Transform, obj.Transform.GameObject.Transform.GameObject.Transform);
            var pos = new Vec3(4, 8, 7);
            obj.SetPosition(pos);
            Assert.AreEqual(obj.Transform, obj.Transform.GameObject.Transform);
            Assert.AreEqual(pos, obj.Transform.GameObject.Transform.Position);
        }
    }
}

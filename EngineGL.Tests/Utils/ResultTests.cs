using EngineGL.Structs.Math;
using EngineGL.Utils;
using NUnit.Framework;

namespace EngineGL.Tests.Utils
{
    [TestFixture]
    class ResultTests
    {
        [Test]
        public void ResultTest()
        {
            var res = ResultHelper.Success("hoge")
                 .Then(str => ResultHelper.Success("fuga"))
                 .Catch<string>(() =>
                 {
                     Assert.Fail();
                     return ResultHelper.Success("この部分は通らないはず");
                 });

            Assert.IsTrue(res.IsSuccess);
            Assert.AreEqual("fuga", res.Value);
        }

        [Test]
        public void ResultTest2()
        {
            var res = ResultHelper.Success(0)
                 .Then(i => ResultHelper.Success(i + 1))
                 .Then(i => ResultHelper.Success(i + 1))
                 .Then(i => ResultHelper.Success(i + 1));

            Assert.IsTrue(res.IsSuccess);
            Assert.AreEqual(3, res.Value);
        }

        [Test]
        public void ResultTest3()
        {
            var res = ResultHelper.Success(0)
                 .Then(i => ResultHelper.Success(i + 1))
                 .Then(i => ResultHelper.Success(i + 1));
            Assert.IsTrue(res.IsSuccess);
            Assert.AreEqual(2, res.Value);

            res = res.Then(i => ResultHelper.Fail<int>());

            Assert.IsFalse(res.IsSuccess);
            res = res.Then(i => ResultHelper.Success(100));
            Assert.IsFalse(res.IsSuccess);
            res = res.Catch<int>(() => ResultHelper.Success(55));
            Assert.IsTrue(res.IsSuccess);
            Assert.AreEqual(55, res.Value);

            int a = res.Match(x => x * 2, () => 777);
            Assert.AreEqual(110, a);
        }


    }
}

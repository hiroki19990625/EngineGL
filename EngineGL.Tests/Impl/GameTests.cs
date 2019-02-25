using EngineGL.Impl;
using NUnit.Framework;

namespace EngineGL.Tests.Impl
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void OpenWindow()
        {
            Game game = new Game();
            game.Run(60.0d);
        }
    }
}
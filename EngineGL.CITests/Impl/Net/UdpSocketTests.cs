using System.Net;
using EngineGL.Core.Net;
using EngineGL.Impl.Net;
using NUnit.Framework;

namespace EngineGL.CITests.Impl.Net
{
    [TestFixture]
    public class UdpSocketTests
    {
        [Test]
        public void Tests()
        {
            IUdpSocket socket = new UdpSocketBuilder(new IPEndPoint(IPAddress.Any, 19132))
                .Broadcast(true)
                .Build();

            Assert.True(socket.Start());
            Assert.True(socket.Send(new IPEndPoint(IPAddress.Broadcast, 19132), new byte[] {0, 1, 2}));
        }
    }
}
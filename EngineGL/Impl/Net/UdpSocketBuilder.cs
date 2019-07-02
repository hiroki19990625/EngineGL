using System.Net;
using EngineGL.Core.Net;

namespace EngineGL.Impl.Net
{
    public class UdpSocketBuilder
    {
        private UdpSocket _socket;

        public UdpSocketBuilder(IPEndPoint endPoint)
        {
            _socket = new UdpSocket(endPoint);
        }

        public UdpSocketBuilder Broadcast(bool val)
        {
            _socket.Broadcast = val;
            return this;
        }

        public UdpSocketBuilder DontFragment(bool val)
        {
            _socket.DontFragment = val;
            return this;
        }

        public UdpSocketBuilder MulticastLoopback(bool val)
        {
            _socket.MulticastLoopback = val;
            return this;
        }

        /*public UdpSocketBuilder ExclusiveAddressUse(bool val)
        {
            _socket.ExclusiveAddressUse = val;
            return this;
        }*/

        public UdpSocketBuilder Ttl(short val)
        {
            _socket.Ttl = val;
            return this;
        }

        public UdpSocketBuilder SendTimeout(int val)
        {
            _socket.SendTimeout = val;
            return this;
        }

        public UdpSocketBuilder ReceiveTimeout(int val)
        {
            _socket.ReceiveTimeout = val;
            return this;
        }

        public ISocket Build()
        {
            return _socket;
        }
    }
}
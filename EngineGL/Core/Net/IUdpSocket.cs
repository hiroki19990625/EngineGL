using System;
using System.Net;

namespace EngineGL.Core.Net
{
    public interface IUdpSocket
    {
        IPEndPoint EndPoint { get; }
        Action<(IPEndPoint endPoint, byte[] buffer)> OnReceive { set; }
        SocketState State { get; }

        bool Broadcast { get; set; }
        bool DontFragment { get; set; }
        bool MulticastLoopback { get; set; }
        short Ttl { get; set; }

        int SendTimeout { get; set; }
        int ReceiveTimeout { get; set; }

        bool Start();
        bool Stop();

        bool Send(IPEndPoint endPoint, byte[] bytes);
    }
}
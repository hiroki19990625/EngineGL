using System;
using System.Net;

namespace EngineGL.Core.Net
{
    public interface ISocket
    {
        IPEndPoint EndPoint { get; }
        Action<(IPEndPoint endPoint, byte[] buffer)> OnReceive { set; }
        SocketState State { get; }

        bool Start();
        bool Stop();

        bool Send(IPEndPoint endPoint, byte[] bytes);
    }
}
using System.Net;
using System.Net.Sockets;

namespace EngineGL.Core.Net
{
    public interface ITcpServerSocket
    {
        IPEndPoint EndPoint { get; }
        SocketState State { get; }

        bool Start();
        bool Stop();

        NetworkStream AcceptClient();
        bool Send(NetworkStream stream, byte[] buffer);
        byte[] Receive(NetworkStream ns);
    }
}
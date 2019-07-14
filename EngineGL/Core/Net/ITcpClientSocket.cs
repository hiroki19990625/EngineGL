using System.Net;

namespace EngineGL.Core.Net
{
    public interface ITcpClientSocket
    {
        IPEndPoint EndPoint { get; }
        SocketState State { get; }

        bool NoDelay { get; set; }

        int SendTimeout { get; set; }
        int ReceiveTimeout { get; set; }

        int ReceiveBufferSize { get; set; }
        int SendBufferSize { get; set; }


        bool Start();
        bool Stop();
        bool Connection(IPEndPoint endPoint);

        bool Send(byte[] buffer);
        byte[] Receive();
    }
}
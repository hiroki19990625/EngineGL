using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using EngineGL.Core.Net;
using NLog;

namespace EngineGL.Impl.Net
{
    public class TcpClientSocket : ITcpClientSocket
    {
        private TcpClient _client;
        private Task _task;
        private CancellationTokenSource _token;
        private ILogger _logger = LogManager.GetCurrentClassLogger();

        public IPEndPoint EndPoint { get; }
        public SocketState State { get; private set; } = SocketState.Waiting;

        public bool NoDelay { get; set; }

        public int SendTimeout { get; set; } = 1000;
        public int ReceiveTimeout { get; set; } = 1000;

        public int ReceiveBufferSize { get; set; } = 1400;
        public int SendBufferSize { get; set; } = 1400;

        public TcpClientSocket(IPEndPoint endPoint)
        {
            EndPoint = endPoint;
        }

        public bool Start()
        {
            try
            {
                State = SocketState.Starting;

                _client = new TcpClient(EndPoint);
                _client.ReceiveTimeout = ReceiveTimeout;
                _client.SendTimeout = SendTimeout;
                _client.NoDelay = NoDelay;
                _client.ReceiveBufferSize = ReceiveBufferSize;
                _client.SendBufferSize = SendBufferSize;
                State = SocketState.Running;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }

            return true;
        }

        public bool Stop()
        {
            try
            {
                _token?.Cancel();
                _client?.Close();
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }

            return true;
        }

        public bool Connection(IPEndPoint endPoint)
        {
            if (State == SocketState.Running)
            {
                _client.Connect(endPoint);
                return true;
            }

            return false;
        }

        public bool Send(byte[] buffer)
        {
            if (State == SocketState.Running)
            {
                _client.GetStream().Write(buffer, 0, buffer.Length);
                return true;
            }

            return false;
        }

        public byte[] Receive()
        {
            if (State == SocketState.Running)
            {
                NetworkStream ns = _client.GetStream();
                using (MemoryStream stream = new MemoryStream())
                {
                    int d = ns.ReadByte();
                    while (d != -1)
                    {
                        stream.WriteByte((byte) d);
                        d = ns.ReadByte();
                    }

                    return stream.ToArray();
                }
            }

            return new byte[0];
        }
    }
}
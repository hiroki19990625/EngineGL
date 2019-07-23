using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using EngineGL.Core.Net;
using NLog;

namespace EngineGL.Impl.Net
{
    public class TcpServerSocket : ITcpServerSocket
    {
        private TcpListener _listener;
        private ILogger _logger = LogManager.GetCurrentClassLogger();

        public IPEndPoint EndPoint { get; }
        public SocketState State { get; private set; } = SocketState.Waiting;

        public TcpServerSocket(IPEndPoint endPoint)
        {
            EndPoint = endPoint;
        }

        public bool Start()
        {
            try
            {
                State = SocketState.Starting;

                _listener = new TcpListener(EndPoint);
                _listener.Start();
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
                _listener.Stop();
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }

            return true;
        }

        public NetworkStream AcceptClient()
        {
            return _listener.AcceptTcpClient().GetStream();
        }

        public bool Send(NetworkStream stream, byte[] buffer)
        {
            if (State == SocketState.Running)
            {
                stream.Write(buffer, 0, buffer.Length);
                return true;
            }

            return false;
        }

        public byte[] Receive(NetworkStream ns)
        {
            if (State == SocketState.Running)
            {
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
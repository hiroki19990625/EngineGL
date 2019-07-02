using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using EngineGL.Core.Net;
using NLog;

namespace EngineGL.Impl.Net
{
    public class UdpSocket : ISocket
    {
        private UdpClient _client;
        private Task _task;
        private CancellationTokenSource _token;
        private ILogger _logger = LogManager.GetCurrentClassLogger();

        public IPEndPoint EndPoint { get; }
        public Action<(IPEndPoint endPoint, byte[] buffer)> OnReceive { private get; set; }
        public SocketState State { get; private set; } = SocketState.Waiting;

        public bool Broadcast { get; set; }

        public bool DontFragment { get; set; } = true;

        public bool MulticastLoopback { get; set; }

        // public bool ExclusiveAddressUse { get; set; }

        public short Ttl { get; set; } = 128;

        public int SendTimeout { get; set; } = 1000;
        public int ReceiveTimeout { get; set; } = 1000;

        public UdpSocket(IPEndPoint endPoint)
        {
            EndPoint = endPoint;
        }

        public bool Start()
        {
            try
            {
                _client = new UdpClient(EndPoint);
                _client.EnableBroadcast = Broadcast;
                _client.DontFragment = DontFragment;
                _client.MulticastLoopback = MulticastLoopback;
                // _client.ExclusiveAddressUse = ExclusiveAddressUse;
                _client.Ttl = Ttl;
                _client.Client.SendTimeout = SendTimeout;
                _client.Client.ReceiveTimeout = ReceiveTimeout;
                State = SocketState.Starting;
                _token = new CancellationTokenSource();
                _task = Task.Factory.StartNew(Receive, _token.Token, TaskCreationOptions.LongRunning,
                    TaskScheduler.Default);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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

        public bool Send(IPEndPoint endPoint, byte[] bytes)
        {
            if (State == SocketState.Running)
            {
                _client.Send(bytes, bytes.Length, endPoint);
                return true;
            }

            return false;
        }

        private void Receive()
        {
            State = SocketState.Running;

            IPEndPoint endPoint = null;
            while (State == SocketState.Running)
            {
                byte[] buffer = _client.Receive(ref endPoint);
                OnReceive?.Invoke((endPoint, buffer));
                if (_token.IsCancellationRequested)
                {
                    _token.Token.ThrowIfCancellationRequested();
                }
            }
        }
    }
}
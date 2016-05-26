using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SocketUtilities.Core;
using SocketUtilities.Messaging;

namespace SocketUtilities.Server
{
    public class InternalServer : ICommunicationServer
    {
        private readonly ILogger _logger;

        public InternalServer()
            : this(IPAddress.Parse("127.0.0.1"), 8888, new FileLogger(Environment.CurrentDirectory))
        {
        }

        public InternalServer(int port)
            : this(IPAddress.Parse("127.0.0.1"), port, new FileLogger(Environment.CurrentDirectory))
        {
        }

        public InternalServer(string ipAddress)
            : this(ipAddress, 8888)
        {
        }

        public InternalServer(string ipAddress, int port)
            : this(IPAddress.Parse(ipAddress), port, new FileLogger(Environment.CurrentDirectory))
        {
        }

        public InternalServer(IPAddress ipAddress)
            : this(ipAddress, 8888, new FileLogger(Environment.CurrentDirectory))
        {
        }

        public InternalServer(IPAddress ipAddress, int port, ILogger logger)
        {
            _logger = logger;

            TcpListener = new TcpListener(ipAddress, port);
            Socket = new Socket(SocketType.Stream, ProtocolType.IP);

            ServerId = Guid.NewGuid();
        }

        public TcpListener TcpListener { get; set; }
        public Socket Socket { get; set; }
        public Guid ServerId { get; set; }
        public void Start()
        {
            try
            {

                TcpListener.Start();

                TcpListener.BeginAcceptSocket(AcceptSocketCallback, null);

            }
            catch(Exception e)
            {
                _logger.Warn(e.Message);
            }
        }


        private void AcceptSocketCallback(IAsyncResult ar)
        {
            Socket socket = TcpListener.EndAcceptSocket(ar);

            ClientConnectedEvent?.Invoke(this);

            Read(socket);
        }

        public void Stop()
        {
            TcpListener.Stop();
        }

        public void Read(Socket socket)
        {
            try
            {
                SocketMessage socketMessage = new SocketMessage();

                socket.BeginReceive(socketMessage.MessageBytes, 0, socketMessage.MessageBytes.Length,
                    SocketFlags.None, Callback, socketMessage);

            }

            catch(Exception e)
            {
                _logger.Warn(e.Message);
            }
        }

        private void Callback(IAsyncResult ar)
        {
            MessageRecievedEvent?.Invoke(this, (SocketMessage) ar.AsyncState);
        }

        public void SendMessage(SocketMessage message)
        {
            try
            {
                TcpListener.Server.BeginSend(message.MessageBytes, 0, message.MessageBytes.Length, SocketFlags.None,
                    ar =>
                    {
                        TcpListener.Server.EndSend(ar);

                    }, null);
            }

            catch(Exception e)
            {
                _logger.Warn(e.Message);
            }
        }

        public event Action<ICommunicationServer> ClientConnectedEvent;

        public event Action<ICommunicationServer, SocketMessage> MessageRecievedEvent;
    }
}
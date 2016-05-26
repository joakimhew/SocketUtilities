using System;
using System.Net;
using System.Net.Sockets;
using SocketUtilities.Messaging;

namespace SocketUtilities.Server
{
    public class InternalServer : ICommunicationServer
    {

        public InternalServer()
            : this(IPAddress.Parse("127.0.0.1"), 8888)
        {
        }

        public InternalServer(string ipAddress)
            : this(ipAddress, 8888)
        {
        }

        public InternalServer(string ipAddress, int port)
            : this(IPAddress.Parse(ipAddress), port)
        {
        }

        public InternalServer(IPAddress ipAddress)
            : this(ipAddress, 8888)
        {
        }

        public InternalServer(IPAddress ipAddress, int port)
        {
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
            catch
            {
                //todo: Log exception
            }
        }


        private void AcceptSocketCallback(IAsyncResult ar)
        {
            TcpListener.EndAcceptSocket(ar);

            ClientConnectedEvent?.Invoke(this);

            Read();
        }

        public void Stop()
        {
            TcpListener.Stop();
        }

        public void Read()
        {
            try
            {
                SocketMessage socketMessage = new SocketMessage();


                TcpListener.Server.BeginReceive(socketMessage.MessageBytes, 0, socketMessage.MessageBytes.Length,
                    SocketFlags.None, Callback, null);
            }

            catch
            {
                //todo: Log exception
            }
        }

        private void Callback(IAsyncResult ar)
        {
            throw new NotImplementedException();
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

            catch
            {
                //todo: Log exception
            }
        }

        public event Action<ICommunicationServer> ClientConnectedEvent;

        public event Action<ICommunicationServer, SocketMessage> MessageRecievedEvent;
    }
}
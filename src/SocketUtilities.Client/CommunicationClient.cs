using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using SocketUtilities.Core;
using SocketUtilities.Messaging;

namespace SocketUtilities.Client
{
    public class CommunicationClient : ICommunicationClient
    {
        private readonly ILogger _logger;
        private readonly object _sendSyncRoot = new object();

        public CommunicationClient()
            : this(new FileLogger(Environment.CurrentDirectory))
        {
            TcpClient = new TcpClient();
            ClientId = Guid.NewGuid();
        }

        public CommunicationClient(ILogger logger)
        {
            _logger = logger;
        }

        public TcpClient TcpClient { get; set; }
        public Guid ClientId { get; set; }

        public void Connect(string ip, int port)
        {
            try
            {
                TcpClient.Client.Connect(ip, port);
                ConnectionEstablishedEvent?.Invoke(this);
                SendIdentification();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
        }

        public void Connect(IPEndPoint endPoint)
        {
            try
            {
                TcpClient.Client.Connect(endPoint);
                ConnectionEstablishedEvent?.Invoke(this);
                SendIdentification();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
        }

        public void BeginConnect(string ip, int port)
        {
            TcpClient.Client.BeginConnect(ip, port, ar =>
            {
                TcpClient.Client.EndConnect(ar);
                ConnectionEstablishedEvent?.Invoke(this);
                SendIdentification();
            }, null);
        }

        public void BeginConnect(IPEndPoint endPoint)
        {
            TcpClient.BeginConnect(endPoint.Address.ToString(), endPoint.Port, ar =>
            {
                TcpClient.Client.EndConnect(ar);
                ConnectionEstablishedEvent?.Invoke(this);
                SendIdentification();
            }, null);
        }

        public void SendIdentification()
        {

        }

        public void Send(ISocketMessage socketMessageBase)
        {
            try
            {
                Socket client = TcpClient.Client;

                if (!client.Connected)
                    return;

                byte[] serialized = socketMessageBase.Serialize();

                client.BeginSend(serialized, 0, serialized.Length, 0, SendMessageCallback, client);
            }

            catch (Exception e)
            {

            }
        }

        private void SendMessageCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket) ar.AsyncState;

                if (client.Connected)
                {
                    int bytesSent = client.EndSend(ar);
                    Debug.WriteLine($"Client sent {bytesSent} bytes");
                }
            }
            catch (SocketException e)
            {
                _logger.Warn(e.Message);
            }
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public event Action<ICommunicationClient> ConnectionEstablishedEvent;
    }
}
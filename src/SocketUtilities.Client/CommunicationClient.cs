using System;
using System.Net;
using System.Net.Sockets;
using SocketUtilities.Messaging;

namespace SocketUtilities.Client
{
    public class CommunicationClient : ICommunicationClient
    {
       
        public CommunicationClient()
        {
            Client = new TcpClient();
            ClientId = Guid.NewGuid();
        }


        public TcpClient Client { get; set; }
        public Guid ClientId { get; set; }

        public void Connect(string ip, int port)
        {
            Client.Client.BeginConnect(ip, port, ar =>
            {
                Client.Client.EndConnect(ar);

                ConnectionEstablishedEvent?.Invoke(this);
            }, null);
        }

        public void Connect(IPEndPoint endPoint)
        {
            Client.BeginConnect(endPoint.Address.ToString(), endPoint.Port, ar =>
            {
                Client.Client.EndConnect(ar);

                ConnectionEstablishedEvent?.Invoke(this);
            }, null);
        }

        public void SendMessage(SocketMessage socketMessage)
        {
            try
            {
                Client.Client.BeginSend(socketMessage.MessageBytes, 0, socketMessage.MessageBytes.Length, SocketFlags.None,
                    ar =>
                    {
                        Client.Client.EndSend(ar);

                    }, null);
            }

            catch
            {
                //todo: Log exception
            }
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public event Action<ICommunicationClient> ConnectionEstablishedEvent;
    }
}
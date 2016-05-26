using System;
using System.Net;
using System.Net.Sockets;
using SocketUtilities.Messaging;

namespace SocketUtilities.Client
{
    public interface ICommunicationClient
    {
        TcpClient Client { get; set; }
        Guid ClientId { get; set; }

        void Connect(string ip, int port);
        void Connect(IPEndPoint endPoint);
        void SendMessage(SocketMessage socketMessage);
        void Disconnect();
        event Action<ICommunicationClient> ConnectionEstablishedEvent;
    }
}
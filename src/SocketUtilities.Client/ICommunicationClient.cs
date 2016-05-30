using System;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;
using SocketUtilities.Messaging;

namespace SocketUtilities.Client
{
    public interface ICommunicationClient
    {
        TcpClient TcpClient { get; set; }
        Guid ClientId { get; set; }

        void Connect(string ip, int port);
        void Connect(IPEndPoint endPoint);

        void BeginConnect(string ip, int port);
        void BeginConnect(IPEndPoint endPoint);
        void SendIdentification();
        void Send(ISocketMessage socketMessageBase);
        void Disconnect();
        event Action<ICommunicationClient> ConnectionEstablishedEvent;
    }
}
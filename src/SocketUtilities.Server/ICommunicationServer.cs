using System;
using System.Net.Sockets;
using SocketUtilities.Messaging;

namespace SocketUtilities.Server
{
    public interface ICommunicationServer
    {
        TcpListener TcpListener { get; set; }
        Socket Socket { get; set; }
        Guid ServerId { get; set; }
        void Start();
        void Stop();

        void Read();

        void SendMessage(SocketMessage message);

        event Action<ICommunicationServer> ClientConnectedEvent;
        event Action<ICommunicationServer, SocketMessage> MessageRecievedEvent;
    }
}
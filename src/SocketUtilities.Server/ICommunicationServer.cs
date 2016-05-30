using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using SocketUtilities.Messaging;

namespace SocketUtilities.Server
{
    public interface ICommunicationServer
    {
        Guid ServerId { get; set; }
        TcpListener TcpListener { get; set; }
        Dictionary<Socket, Guid> Clients { get; set; }
        Socket Socket { get; set; }
        void Start();
        void Stop();
        void StartListening();
        void Send(Socket socket, ISocketMessage messageBase);
        void Broadcast(ISocketMessage messageBase);
        event Action<Socket> ClientConnectedEvent;
        event Action<ICommunicationServer, Guid> ClientIdentificationEvent;
        event Action<ICommunicationServer, ISocketMessage> MessageRecievedEvent;
    }
}
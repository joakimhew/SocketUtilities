using SocketUtilities.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SocketUtilities.Client;
using SocketUtilities.Messaging;

namespace TestProgram
{
    class Program
    {
        static readonly InternalServer InternalServer = new InternalServer(5000);
        static readonly ICommunicationClient CommunicationClient = new CommunicationClient();

        private static void Main(string[] args)
        {
            var clients = new Dictionary<Socket, Guid>();

            // Adds a handler for the ClientConnectedEvent, this is fired everytime a client connects to the InternalServer EndPoint
            InternalServer.ClientConnectedEvent += delegate (Socket socket)
            {
                Console.WriteLine($"Client connected: {socket.RemoteEndPoint}");
            };

            // Adds a handler for the ClientIdentificationEvent, this is fired right after a client is connected.
            InternalServer.ClientIdentificationEvent += delegate (ICommunicationServer server, Guid clientId)
            {

            };


            // Adds a handler for the MessageRecievedEvent, this is fired everytime the server sucessfully reads a message.  
            InternalServer.MessageRecievedEvent += delegate (ICommunicationServer server, ISocketMessage message)
            {
                Console.WriteLine($"Message recieved from client: {server.Socket.RemoteEndPoint}");
                Console.WriteLine("---Message start---");
                Console.WriteLine($"Message type: {message.MessageType}");
                Console.WriteLine($"Message: {message.Message}");
                Console.WriteLine("---Message end---");
                Console.WriteLine();
            };

            InternalServer.Start();
            StartListening();
            ConnectTestClient();

            while (true)
            {

                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    SendTestMessages();
                }

                Console.WriteLine("\n\n\n");
                Console.WriteLine("Press enter to send 50 messages from client");
            }
        }

        public static void StartListening()
        {
            Console.WriteLine("Waiting for connection...");
            InternalServer.StartListening();
        }

        public static void ConnectTestClient()
        {
            CommunicationClient.BeginConnect("127.0.0.1", 5000);
        }

        private static void SendTestMessages()
        {
            ISocketMessage socketMessage = new StandardSocketMessage();

            for (int i = 0; i < 50; i++)
            {
                socketMessage.Message = $"Hello from client! Message #{i+1}";
                CommunicationClient.Send(socketMessage);
            }
        }

    }
}

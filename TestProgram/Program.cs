using SocketUtilities.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SocketUtilities.Client;
using SocketUtilities.Messaging;

namespace TestProgram
{
    class Program
    {
        private static void Main(string[] args)
        {
            bool clientIsConnected = false;

            InternalServer internalServer = new InternalServer(5000);

            internalServer.ClientConnectedEvent += delegate(ICommunicationServer server)
            {
                clientIsConnected = true;
            };

            internalServer.MessageRecievedEvent += delegate(ICommunicationServer server, SocketMessage message)
            {
                Console.WriteLine(message.MessageString);
            };

            ICommunicationClient communicationClient = new CommunicationClient();

            internalServer.Start();
            Console.WriteLine("Waiting for connection...");

            Thread.Sleep(2000);
            communicationClient.Connect("127.0.0.1", 5000);
            Console.WriteLine("Client sucessfully connected!");

            byte[] messageBytes = {154,1,120};

            communicationClient.SendMessage(new SocketMessage(messageBytes));


            Console.ReadKey();
        }
    }
}

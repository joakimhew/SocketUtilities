using System.Net;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocketUtilities.Client;
using SocketUtilities.Server;

namespace SocketUtilities.CommunicationTests
{
    [TestClass]
    public class ServerTests
    {
        private ICommunicationServer _server;

        //[TestInitialize]
        //public void InitializeServerAndClientConnection()
        //{
        //    _server = new InternalServer(5000);
        //    _server.Start();
        //    _server.StartListening();

        //    ICommunicationClient client = new CommunicationClient();
        //    client.Connect("127.0.0.1", 5000);
        //}

        //[TestMethod]
        //public void Server_Can_Start_With_Default_Settings()
        //{
        //    EndPoint expectedEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
        //    EndPoint actualEndPoint = _server.TcpListener.LocalEndpoint;

        //    Assert.IsTrue(_server.TcpListener.Server.IsBound);
        //    Assert.AreEqual(expectedEndPoint, actualEndPoint);
        //}

        //[TestMethod]
        //public void Default_BufferSize_For_Reading_Is_Set_To_2048()
        //{
        //    _server.Read();

        //    int expectedBufferSize = 2048;
        //    int actualBufferSize = _server.Socket.ReceiveBufferSize;

        //    Assert.AreEqual(expectedBufferSize, actualBufferSize);
        //}
    }
}
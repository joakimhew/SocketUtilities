using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using SocketUtilities.Core;
using SocketUtilities.Messaging;

namespace SocketUtilities.Server
{
    public class InternalServer : ICommunicationServer
    {
        private readonly ILogger _logger;
        private readonly ISocketMessage _socketMessage;


        public InternalServer()
            : this(IPAddress.Parse("127.0.0.1"), 8888, new FileLogger(Environment.CurrentDirectory))
        {
        }

        public InternalServer(int port)
            : this(IPAddress.Parse("127.0.0.1"), port, new FileLogger(Environment.CurrentDirectory))
        {
        }

        public InternalServer(string ipAddress)
            : this(ipAddress, 8888)
        {
        }

        public InternalServer(string ipAddress, int port)
            : this(IPAddress.Parse(ipAddress), port, new FileLogger(Environment.CurrentDirectory))
        {
        }

        public InternalServer(IPAddress ipAddress)
            : this(ipAddress, 8888, new FileLogger(Environment.CurrentDirectory))
        {
        }

        public InternalServer(IPAddress ipAddress, int port, ILogger logger)
        {
            _logger = logger;

            TcpListener = new TcpListener(ipAddress, port);
            _socketMessage = new StandardSocketMessage();
            Clients = new Dictionary<Socket, Guid>();

            ServerId = Guid.NewGuid();
        }



        public TcpListener TcpListener { get; set; }
        public Socket Socket { get; set; }
        public Dictionary<Socket, Guid> Clients { get; set; }
        public Guid ServerId { get; set; }


        public void Start()
        {
            try
            {
                TcpListener.Start();
            }
            catch (Exception e)
            {
                _logger.Warn(e.Message);
            }
        }

        public void Stop()
        {
            TcpListener.Stop();
        }

        public void StartListening()
        {
            Socket = TcpListener.Server;
            Socket.Listen(100);
            Socket.BeginAccept(AcceptCallback, Socket);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Socket listener = (Socket)ar.AsyncState;
            Socket = listener.EndAccept(ar);

            try
            {
                Clients.Add(Socket, Guid.Empty);
                ClientConnectedEvent?.Invoke(Socket);

                Receive(Socket);
            }
            catch (SocketException s)
            {
                _logger.Error(s.Message);
            }
            finally
            {
                StartListening();
            }
        }

        private void Receive(Socket socket)
        {
            StateObject state = new StateObject();
            state.WorkSocket = socket;
            state.BufferSize = 8192;

            socket.BeginReceive(state.Buffer, 0, state.BufferSize, 0, ReadCallback, state);
            
        }

        public void ReadCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket = state.WorkSocket;
            ISocketMessage message = new StandardSocketMessage();

            try
            {
                int bytesRead = Socket.EndReceive(ar);

                if (bytesRead > 0)
                {
                    foreach (var msg in message.Deserialize(state.Buffer))
                    {
                        switch (msg.MessageType)
                        {
                            case SocketMessageType.Methods:
                                break;
                            case SocketMessageType.MethodExecution:
                                break;
                            case SocketMessageType.Identity:
                                ClientIdentificationEvent?.Invoke(this, Guid.Parse(msg.Message));
                                break;
                            case SocketMessageType.Normal:
                                MessageRecievedEvent?.Invoke(this, msg);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }

                    Socket.BeginReceive(state.Buffer, 0, state.BufferSize, 0, ReadCallback, state);
                }

                else
                {
                }
            }
            catch (SocketException e)
            {
                _logger.Warn(e.Message);
            }
        }


        public void Send(Socket socket, ISocketMessage messageBase)
        {
            try
            {
                if (socket.Connected)
                {
                    byte[] serialized = messageBase.Serialize();

                    Socket.BeginSendTo(serialized, 0, serialized.Length, 0, socket.RemoteEndPoint, SendCallback, socket);
                }
            }
            catch (SocketException e)
            {
                _logger.Warn(e.Message);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket socket = (Socket) ar.AsyncState;
                socket.EndSendTo(ar);
            }
            catch (Exception e)
            {
                _logger.Warn(e.Message);
            }
        }

        public void Broadcast(ISocketMessage messageBase)
        {
            throw new NotImplementedException();
        }


        public void Send(ISocketMessage messageBase)
        {
            try
            {
            }

            catch (Exception e)
            {
                _logger.Warn(e.Message);
            }
        }

        public event Action<Socket> ClientConnectedEvent;

        public event Action<ICommunicationServer, Guid> ClientIdentificationEvent;

        public event Action<ICommunicationServer, ISocketMessage> MessageRecievedEvent;
    }
}
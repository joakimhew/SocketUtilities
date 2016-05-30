using System;
using System.Net.Sockets;
using System.Text;

namespace SocketUtilities.Core
{
    public class StateObject
    {
        public Socket Socket = null;

        private int _bufferSize;
        public int ReadOffset = 0;

        public StringBuilder StringBuilder = new StringBuilder();

        public StateObject(int bufferSize = 2048)
        {
            BufferSize = bufferSize;
        }

        public int BufferSize
        {
            set
            {
                _bufferSize = value;
                Buffer = new byte[_bufferSize];
            }

            get { return _bufferSize; }
        }

        public byte[] Buffer { get; set; }
    }
}
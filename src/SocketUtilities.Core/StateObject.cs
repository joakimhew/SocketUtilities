using System;
using System.Net.Sockets;
using System.Text;

namespace SocketUtilities.Core
{
    public class StateObject
    {
        public Socket WorkSocket = null;
        public int ReadOffset = 0;
        public StringBuilder sb = new StringBuilder();

        private int _bufferSize = 0;
        public int BufferSize
        {
            set
            {
                _bufferSize = value;
                _buffer = new byte[_bufferSize];
            }

            get { return _bufferSize; }
        }

        private byte[] _buffer;
        public byte[] Buffer => _buffer;
    }
}
using System;
using System.Text;

namespace SocketUtilities.Messaging
{
    /// <summary>
    /// Used to send structured messages through the socket
    /// Default encoding is Encoding.UTF8 and default type is <see cref="SocketMessageType.Normal">Normal</see>
    /// </summary>
    public class SocketMessage
    {
        private string _messageString;
        private byte[] _messageBytes;
        private Encoding _encoding;
        public SocketMessage(int messageBufferSize = 2048)
            : this(Encoding.UTF8, SocketMessageType.Normal, String.Empty, messageBufferSize)
        {
        }


        /// <summary>
        /// Initialize a new instance of SocketMessage using default type and encoding with a message of type <see cref="String">String</see>
        /// </summary>
        /// <param name="message">User provided message</param>
        /// <param name="messageBufferSize"></param>
        public SocketMessage(string message, int messageBufferSize = 2048)
            : this(Encoding.UTF8, SocketMessageType.Normal, message, messageBufferSize)
        {    
        }

        /// <summary>
        /// Initialize a new instance of SocketMessage using default type and encoding with a message of type <see cref="Byte">Byte</see>
        /// </summary>
        /// <param name="message">User provided message</param>
        public SocketMessage(byte[] message)
            : this(Encoding.UTF8, SocketMessageType.Normal, message)
        {
        }

        /// <summary>
        /// Initialize a new instance of SocketMessage using default encoding with a message of type <see cref="String">String</see>
        /// </summary>
        /// <param name="type">Type of message. Explanation for each type found at <see cref="SocketMessageType"/></param>
        /// <param name="message">User provided message</param>
        /// <param name="messageBufferSize"></param>
        public SocketMessage(SocketMessageType type, string message, int messageBufferSize = 2048)
            : this(Encoding.UTF8, type, message, messageBufferSize)
        {
        }


        /// <summary>
        /// Initialize a new instance of SocketMessage using default encoding with a message of type <see cref="Byte">Byte</see>
        /// </summary>
        /// <param name="type">Type of message. Explanation for each type found at <see cref="SocketMessageType"/></param>
        /// <param name="message">User provided message</param>
        /// <param name="messageBufferSize"></param>
        public SocketMessage(SocketMessageType type, byte[] message, int messageBufferSize = 2048)
            : this(Encoding.UTF8, type, message, messageBufferSize)
        {
        }

        /// <summary>
        /// Initialize a new instance of SocketMessage using default type with a message of type <see cref="String">String</see>
        /// </summary>
        /// <param name="encoding">The encoding that will be used for conversion between string and byte message</param>
        /// <param name="message">User provided message</param>
        /// <param name="messageBufferSize"></param>
        public SocketMessage(Encoding encoding, string message, int messageBufferSize = 2048)
            : this(encoding, SocketMessageType.Normal, message, messageBufferSize)
        {
        }

        /// <summary>
        /// Initialize a new instance of SocketMessage using default type with a message of type <see cref="Byte">Byte</see>
        /// </summary>
        /// <param name="encoding">The encoding that will be used for conversion between string and byte message</param>
        /// <param name="message">User provided message</param>
        /// <param name="messageBufferSize"></param>
        public SocketMessage(Encoding encoding, byte[] message, int messageBufferSize = 2048)
            : this(encoding, SocketMessageType.Normal, message, messageBufferSize)
        {
        }

        /// <summary>
        /// Initialize a new instance of SocketMessage with a message of type <see cref="Byte">Byte</see>
        /// </summary>
        /// <param name="encoding">The encoding that will be used for conversion between string and byte message</param>
        /// <param name="type">Type of message. Explanation for each type found at <see cref="SocketMessageType"/></param>
        /// <param name="message">User provided message</param>
        /// <param name="messageBufferSize"></param>
        public SocketMessage(Encoding encoding, SocketMessageType type, byte[] message, int messageBufferSize = 2048)
        {
            if(message.Length == 0)
                throw new ArgumentException("The message array cannot be empty", nameof(message));

            Encoding = encoding;
            Type = type;
            MessageBytes = message;

            MessageBytes = new byte[messageBufferSize];

        }

        /// <summary>
        /// Initialize a new instance of SocketMessage with a message of type <see cref="String">String</see>
        /// </summary>
        /// <param name="encoding">The encoding that will be used for conversion between string and byte message</param>
        /// <param name="type">Type of message. Explanation for each type found at <see cref="SocketMessageType"/></param>
        /// <param name="message">User provided message</param>
        /// <param name="messageBufferSize"></param>
        public SocketMessage(Encoding encoding, SocketMessageType type, string message, int messageBufferSize = 2048)
        {
            if(message == null)
                throw new ArgumentNullException(nameof(message), "The message cannot be null");

            Encoding = encoding;
            Type = type;
            MessageString = message;

            MessageBytes = new byte[messageBufferSize];
        }

        /// <summary>
        /// Encoding used for conversion between <see cref="MessageBytes"/> and <see cref="MessageString"/>
        /// </summary>
        public Encoding Encoding { get; }

       

        /// <summary>
        /// The type of message.
        /// </summary>
        public SocketMessageType Type { get; set; }

        /// <summary>
        /// User provided message in byte[]. If message was provided as string, this will be set by the <see cref="Encoding"/>
        /// </summary>
        public byte[] MessageBytes
        {
            get { return _messageBytes; }
            set
            {
                _messageBytes = value;
                _messageString = Encoding.GetString(value);
            }
        }


        /// <summary>
        ///  /// User provided message in string. If message was provided as byte[], this will be set by the <see cref="Encoding"/>
        /// </summary>
        public string MessageString
        {
            get { return _messageString; }
            set
            {
                _messageString = value;
                _messageBytes = Encoding.GetBytes(value);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SocketUtilities.Core;

namespace SocketUtilities.Messaging
{
    public class StandardSocketMessage : ISocketMessage
    {

        private readonly byte[] _eof;

        public StandardSocketMessage()
            : this(Encoding.ASCII, SocketMessageType.Normal)
        {

        }

        public StandardSocketMessage(SocketMessageType messageType)
            : this(Encoding.ASCII, messageType)
        {

        }

        public StandardSocketMessage(Encoding encoding, SocketMessageType messageType)
        {
            Encoding = encoding;
            MessageType = messageType;

            _eof = Encoding.GetBytes("<EOF>");
        }

        [JsonIgnore]
        public Encoding Encoding { get; set; }

        [JsonProperty]
        public SocketMessageType MessageType { get; set; }

        [JsonProperty]
        public string Message { get; set; }

        /// <summary>
        /// Deserialize the buffer into instances of ISocketMessage. 
        /// </summary>
        /// <param name="buffer">The buffer recieved from socket that should be deserialized</param>
        /// <returns>A collection of ISockedMessages</returns>
        public IEnumerable<ISocketMessage> Deserialize(byte[] buffer)
        {
            // Initialize a stack object for populating from the buffer.
            byte[] dataStack = new byte[buffer.Length];


            int readBytes = -1;

            // Loop through the buffer adding each element to the dataStack. 
            for (int pos = 0; pos < buffer.Length; pos++)
            {

                readBytes++;
                Array.Copy(buffer, pos, dataStack, pos, 1);

                // If the current byte is not the start of a EOF, keep adding data to the stack.
                if (!buffer[pos].Equals(_eof[0]))
                    continue;

                bool isEof = false;

                //Check if it is really a EOF or not
                for (int i = 0; i < _eof.Length; i++)
                {
                    isEof = buffer[pos + i].Equals(_eof[i]);
                }

                if (!isEof)
                    continue;

                byte[] messageBytes;

                if (pos - readBytes == 0)
                {
                    messageBytes = new byte[readBytes];
                    Array.ConstrainedCopy(buffer, pos - readBytes, messageBytes, 0, readBytes);
                    readBytes = 0;
                }

                else
                {
                    int messageLength = readBytes - _eof.Length;
                    messageBytes = new byte[messageLength];
                    Array.ConstrainedCopy(buffer, pos - messageLength, messageBytes, 0, messageLength);
                    readBytes = 0;
                }


                string messageString = Encoding.GetString(messageBytes);
                JsonSerializer jsonSerializer = new JsonSerializer();
                TextReader textReader = new StringReader(messageString);
                JsonReader jsonReader = new JsonTextReader(textReader);

                yield return
                    jsonSerializer.Deserialize<StandardSocketMessage>(jsonReader);
            }
        }

        /// <summary>
        /// Serializes the current instance of ISocketMessage into a json string.
        /// </summary>
        /// <returns>The json string in byte[]</returns>
        public byte[] Serialize()
        {
            byte[] jsonBytes = Encoding.GetBytes(JsonConvert.SerializeObject(this));

            byte[] data = new byte[jsonBytes.Length + _eof.Length];

            Array.Copy(jsonBytes, 0, data, 0, jsonBytes.Length);
            Array.Copy(_eof, 0, data, jsonBytes.Length, _eof.Length);

            return data;
        }
    }

    public static class ByteExtensions
    {
        public static bool IsSame(this byte[] sourceBytes, byte[] destBytes)
        {
            return !sourceBytes.Where((t, i) => t != destBytes[i]).Any();
        }
    }
}
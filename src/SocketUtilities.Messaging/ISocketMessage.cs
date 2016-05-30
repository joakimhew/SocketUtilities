using System.Collections.Generic;
using System.Text;
using SocketUtilities.Core;

namespace SocketUtilities.Messaging
{
    public interface ISocketMessage
    {
        Encoding Encoding { get; set; }
        SocketMessageType MessageType { get; set; }
        string Message { get; set; }
        IEnumerable<ISocketMessage> Deserialize(byte[] buffer); 
        byte[] Serialize();
    }
}
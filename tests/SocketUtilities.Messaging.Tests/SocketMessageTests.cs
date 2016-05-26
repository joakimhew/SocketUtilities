using System;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocketUtilities.Messaging;

namespace RpcSocket.Messaging.Tests
{
    [TestClass]
    public class SocketMessageTests
    {
        [TestMethod]
        public void Is_default_encoding_UTF8_when_message_and_type_is_provided()
        {
            SocketMessageType type = SocketMessageType.Identity;
            SocketMessage socketMessage = new SocketMessage(type, "This is a test message");

            var actual = socketMessage.Encoding;
            var expected = Encoding.UTF8;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void Is_default_encoding_UTF8_when_message_is_provided()
        {
            SocketMessage socketMessage = new SocketMessage("This is a test message");

            var actual = socketMessage.Encoding;
            var expected = Encoding.UTF8;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void MessageBytes_and_MessageString_is_set_to_same_when_string_is_provided()
        {
            SocketMessage socketMessage = new SocketMessage("This is a test message");
            Encoding encoding = socketMessage.Encoding;

            var actual = socketMessage.MessageBytes;
            var expected = encoding.GetBytes(socketMessage.MessageString);

            Assert.IsTrue(ByteArrayCompare(actual, expected));
        }

        [TestMethod]
        public void MessageBytes_and_MessageString_is_set_to_same_when_byte_is_provided()
        {
            SocketMessage socketMessage = new SocketMessage(new byte[] {2, 4});
            Encoding encoding = socketMessage.Encoding;

            var actual = socketMessage.MessageBytes;
            var expected = encoding.GetBytes(socketMessage.MessageString);

            Assert.IsTrue(ByteArrayCompare(actual, expected));
        }

        [TestMethod]
        public void MessageBytes_is_set_when_MessageString_is_set_using_UTF8()
        {
            string a = "a";
            Encoding encoding = Encoding.UTF8;
            SocketMessage socketMessage = new SocketMessage(encoding, a);

            var actual = socketMessage.MessageBytes;
            var expected = encoding.GetBytes(socketMessage.MessageString);

            Assert.IsTrue(ByteArrayCompare(actual, expected));
        }

        [TestMethod]
        public void MessageBytes_is_set_when_MessageString_is_set_using_ASCII()
        {
            string a = "a";
            Encoding encoding = Encoding.ASCII;
            SocketMessage socketMessage = new SocketMessage(encoding, a);

            var actual = socketMessage.MessageBytes;
            var expected = encoding.GetBytes(a);

            Assert.IsTrue(ByteArrayCompare(actual, expected));
        }


        [TestMethod]
        public void MessageBytes_is_set_when_MessageString_is_set_using_BigEndianUnicode()
        {
            string a = "a";
            Encoding encoding = Encoding.BigEndianUnicode;
            SocketMessage socketMessage = new SocketMessage(encoding, a);

            var actual = socketMessage.MessageBytes;
            var expected = encoding.GetBytes(a);

            Assert.IsTrue(ByteArrayCompare(actual, expected));
        }


        [TestMethod]
        public void MessageString_is_set_when_MessageBytes_is_set_explicitly()
        {
            SocketMessage socketMessage = new SocketMessage(new byte[] {4, 5, 2});
            socketMessage.MessageBytes = new byte[] {5, 3, 6};
            byte[] stringToBytes = socketMessage.Encoding.GetBytes(socketMessage.MessageString);

            Assert.IsTrue(ByteArrayCompare(stringToBytes, socketMessage.MessageBytes));
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Exception_is_thrown_if_message_is_null()
        {
            string a = null;
            SocketMessage socketMessage = new SocketMessage(SocketMessageType.Normal, a);
            Debug.Write(socketMessage.MessageString);
            Debug.Write(socketMessage.MessageBytes);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void Exception_is_thrown_if_message_is_empty()
        {
            string a = "";
            SocketMessage socketMessage = new SocketMessage(SocketMessageType.Normal, a);
            Debug.Write(socketMessage.MessageString);
            Debug.Write(socketMessage.MessageBytes);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void Exception_is_thrown_if_message_byte_array_is_empty()
        {
            SocketMessage socketMessage = new SocketMessage(SocketMessageType.Normal, new byte[0]);
            Debug.Write(socketMessage.MessageString);
            Debug.Write(socketMessage.MessageBytes);
        }

        private bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            if (a1.Length != a2.Length)
                return false;

            for (int i = 0; i < a1.Length; i++)
            {
                if (a1[i] != a2[i])
                    return false;
            }

            return true;
        }
    }
}

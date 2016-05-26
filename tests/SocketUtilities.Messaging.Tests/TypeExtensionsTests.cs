using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocketUtilities.Messaging.Extensions;

namespace RpcSocket.Messaging.Tests
{
    [TestClass]
    public class TypeExtensionsTests
    {
        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Is_argument_null_exception_thrown_when_type_is_null_in_GetAllMethodSignatures_extension()
        {
            Type type = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            type.GetAllMethodSignatures();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Is_argument_null_exception_thrown_when_type_is_null_in_GetMethodSignature_extension()
        {
            Type type = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            type.GetMethodSignature("DoSomething");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void Is_argument_exception_thrown_when_string_is_empty_in_GetMethodSignature_extension()
        {
            Type type = this.GetType();
            type.GetMethodSignature("");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Is_argument_exception_thrown_when_string_is_null_in_GetMethodSignature_extension()
        {
            Type type = this.GetType();
            type.GetMethodSignature(null);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SocketUtilities.Messaging;

namespace RpcSocket.Messaging.Tests
{
    [TestClass]
    public class MethodSignatureTests
    {
        [TestMethod]
        [TestCategory("Initialization")]
        public void Can_initialize_MethodSignature_from_method_info()
        {
            Type currentType = GetType();
            MethodInfo testMethodInfo = currentType.GetMethod("TestMethod");

            MethodSignatureDto methodSignature = MethodSignatureDto.FromMethod(testMethodInfo);

            Assert.IsNotNull(methodSignature);
        }

        [TestMethod]
        [TestCategory("Transfering")]
        public void Is_MethodSignature_name_set()
        {
            Type currentType = GetType();
            MethodInfo testMethodInfo = currentType.GetMethod("TestMethod");

            MethodSignatureDto methodSignature = MethodSignatureDto.FromMethod(testMethodInfo);

            Assert.AreEqual(testMethodInfo.Name, methodSignature.MethodName);
        }

        [TestMethod]
        [TestCategory("Transfering")]
        public void Is_declaring_type_correct()
        {
            Type currentType = GetType();
            MethodInfo testMethodInfo = currentType.GetMethod("TestMethod");
            MethodSignatureDto methodSignature = MethodSignatureDto.FromMethod(testMethodInfo);

            var actualAssemblyName = methodSignature.DeclaringType.Assemblyname;
            var expectedAssemblyName = currentType.Assembly.FullName;

            var actualClassName = methodSignature.DeclaringType.ClassName;
            var expectedClassName = currentType.FullName;

            Assert.AreEqual(actualAssemblyName, expectedAssemblyName);
            Assert.AreEqual(actualClassName, expectedClassName);
        }




        [TestMethod]
        [TestCategory("Exceptions")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Is_exception_thrown_when_methodinfo_is_null()
        {
            MethodSignatureDto methodSignature = MethodSignatureDto.FromMethod(null);

            Assert.IsNull(methodSignature);
        }

        [TestMethod]
        [TestCategory("Exceptions")]
        [ExpectedException(typeof (ArgumentException))]
        public void Is_argument_exception_thrown_when_MethodsDeserialization_string_is_empty()
        {
            string json = "";
            MethodSignatureDto.DeserializeMethodArray(json);
        }

        [TestMethod]
        [TestCategory("Exceptions")]
        [ExpectedException(typeof(ArgumentException))]
        public void Is_argument_exception_thrown_when_MethodDeserialization_string_is_empty()
        {
            string json = "";
            MethodSignatureDto.DeserializeMethodObject(json);
        }

        [TestMethod]
        [TestCategory("Exceptions")]
        [ExpectedException(typeof(ArgumentException))]
        public void Is_argument_exception_thrown_when_MethodsDeserialization_string_is_null()
        {
            MethodSignatureDto.DeserializeMethodArray(null);
        }

        [TestMethod]
        [TestCategory("Exceptions")]
        [ExpectedException(typeof(ArgumentException))]
        public void Is_argument_exception_thrown_when_MethodDeserialization_string_is_null()
        {
            MethodSignatureDto.DeserializeMethodObject(null);
        }





        [TestMethod]
        [TestCategory("JSON Exceptions")]
        [ExpectedException(typeof(JsonReaderException))]
        public void Is_jsonreader_exception_thrown_when_MethodsDeserialization_string_is_invalid()
        {
            string invalidJson = "this is not valid json";
            MethodSignatureDto.DeserializeMethodArray(invalidJson);
        }

        [TestMethod]
        [TestCategory("JSON Exceptions")]
        [ExpectedException(typeof(JsonReaderException))]
        public void Is_jsonreader_exception_thrown_when_MethodDeserialization_string_is_invalid()
        {
            string invalidJson = "this is not valid json";
            MethodSignatureDto.DeserializeMethodObject(invalidJson);
        }

        [TestMethod]
        [TestCategory("JSON Exceptions")]
        [ExpectedException(typeof(JsonSerializationException))]
        public void Is_jsonserialization_exception_thrown_when_json_array_is_parsed_to_MethodDeserialization()
        {
            string invalidJson = "[{\"IsMember\" : true, \"Name\" : \"John\", \"Age\" : 24}]";
            MethodSignatureDto.DeserializeMethodObject(invalidJson);
        }


        [TestMethod]
        [TestCategory("JSON Exceptions")]
        [ExpectedException(typeof(JsonSerializationException))]
        public void Is_jsonserialization_exception_thrown_when_json_object_is_parsed_to_MethodsDeserialization()
        {
            string invalidJson = "{\"IsMember\" : true, \"Name\" : \"John\", \"Age\" : 24}";
            MethodSignatureDto.DeserializeMethodArray(invalidJson);
        }

        [TestMethod]
        [TestCategory("JSON Exceptions")]
        public void Is_subscribing_event_raised_when_jsonreader_exception_is_thrown()
        {
            List<string> recievedEvents = new List<string>();

            MethodSignatureDto.DeserializingMethodFailedEvent += delegate(object sender, Exception e)
            {
                recievedEvents.Add(e.Message);
            };

            Is_jsonreader_exception_thrown_when_MethodDeserialization_string_is_invalid();
            Is_jsonreader_exception_thrown_when_MethodDeserialization_string_is_invalid();

            Assert.AreEqual(2, recievedEvents.Count);
        }

        [TestMethod]
        [TestCategory("JSON Exceptions")]
        public void Is_subscribing_event_raised_when_jsonserialization_exception_is_thrown()
        {
            List<string> recievedEvents = new List<string>();

            MethodSignatureDto.DeserializingMethodFailedEvent += delegate (object sender, Exception e)
            {
                recievedEvents.Add(e.Message);
            };

            Is_jsonserialization_exception_thrown_when_json_object_is_parsed_to_MethodsDeserialization();
            Is_jsonserialization_exception_thrown_when_json_object_is_parsed_to_MethodsDeserialization();

            Assert.AreEqual(2, recievedEvents.Count);
        }

        public void TestMethod()
        {
            
        }
    }
}
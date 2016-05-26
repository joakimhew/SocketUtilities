using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using SocketUtilities.Messaging.Attributes;

namespace SocketUtilities.Messaging
{
    /// <summary>
    /// Dto for methodinfo, needed for JSON serialization and deserialization
    /// </summary>
    public class MethodSignatureDto
    {
        /// <summary>
        ///  The declaring type for the method
        /// </summary>
        public TypeDto DeclaringType { get; set; }

        /// <summary>
        /// The name of the provided method
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// The parameter types for the method
        /// </summary>
        public TypeDto[] ParameterTypes { get; set; }

        /// <summary>
        /// The AdditionalMethodInfoAttribute for the method. Null if not set
        /// </summary>
        public AdditionalMethodInfoAttribute CustomAttribute { get; set; }

        /// <summary>
        /// Returns a new isntance of MethodSignatureDto.
        /// </summary>
        /// <param name="methodInfo">The method info to initialize from</param>
        /// <returns></returns>
        public static MethodSignatureDto FromMethod(MethodInfo methodInfo)
        {

            if(methodInfo == null)
                throw new ArgumentNullException(nameof(methodInfo), "The methodinfo cannot be null");


            return new MethodSignatureDto
            {
                DeclaringType = TypeDto.FromType(methodInfo.DeclaringType),
                MethodName = methodInfo.Name,
                CustomAttribute = methodInfo.GetCustomAttribute<AdditionalMethodInfoAttribute>(),
                ParameterTypes = methodInfo.GetParameters().Select(t => TypeDto.FromType(t.ParameterType)).ToArray()

            };
        }


        /// <summary>
        /// Desserialize MethodSignatureDto array using JSON
        /// </summary>
        /// <param name="json">The JSON string to deserialize</param>
        /// <returns>Deserialized array of MethodSignatureDTO if JSON is valid. 
        /// Otherwise tries to fire the event <see cref="DeserializingMethodFailedEvent"/></returns>
        public static IEnumerable<MethodSignatureDto> DeserializeMethodArray(string json)
        {
            if (String.IsNullOrEmpty(json))
                throw new ArgumentException("The json string cannot be null or empty", nameof(json));

            try
            {
                return JsonConvert.DeserializeObject<IEnumerable<MethodSignatureDto>>(json);
            }
            catch (JsonReaderException e)
            {
                if (DeserializingMethodFailedEvent == null)
                    throw;

                DeserializingMethodFailedEvent.Invoke(json, e);
                return null;
            }
            catch (JsonSerializationException e)
            {
                if (DeserializingMethodFailedEvent == null)
                    throw;

                DeserializingMethodFailedEvent.Invoke(json, e);
                return null;
            }
        }


        /// <summary>
        /// Desserialize MethodSignatureDto object using JSON
        /// </summary>
        /// <param name="json">The JSON string to deserialize</param>
        /// <returns>Deserialized object of MethodSignatureDTO if JSON is valid. 
        /// Otherwise tries to fire the event <see cref="DeserializingMethodFailedEvent"/></returns>
        public static MethodSignatureDto DeserializeMethodObject(string json)
        {
            if (String.IsNullOrEmpty(json))
                throw new ArgumentException("The json string cannot be null or empty", nameof(json));

            try
            {
                return JsonConvert.DeserializeObject<MethodSignatureDto>(json);
            }
            catch (JsonReaderException e)
            {
                if (DeserializingMethodFailedEvent == null)
                    throw;

                DeserializingMethodFailedEvent.Invoke(json, e);
                return null;
            }
            catch (JsonSerializationException e)
            {
                if (DeserializingMethodFailedEvent == null)
                    throw;

                DeserializingMethodFailedEvent.Invoke(json, e);
                return null;
            }
        }
        /// <summary>
        /// Serialize MethodSignatureDto object into JSON
        /// </summary>
        /// <param name="methodSignature">The MethodSignatureDto to serialize</param>
        /// <returns>Serialized JSON string if MethodSignatureDto is not null</returns>
        public static string SerializeMethodObject(MethodSignatureDto methodSignature)
        {
            if(methodSignature == null)
                throw new ArgumentNullException(nameof(methodSignature), "The provided methodSignature cannot be null");

            return JsonConvert.SerializeObject(methodSignature);
        }

        /// <summary>
        /// Serialize MethodSignatureDto array into JSON
        /// </summary>
        /// <param name="methodSignatures">The MethodSignatureDto array to serialize</param>
        /// <returns>Serialized JSON string if MethodSignatureDto array is not null or empty</returns>
        public static string SerializeMethodArray(IEnumerable<MethodSignatureDto> methodSignatures)
        {
            if(methodSignatures == null)
                throw new ArgumentNullException(nameof(methodSignatures), "The provided methodSignatures cannot be null");

            if(!methodSignatures.Any())
                throw new ArgumentException("The provided methodSignatures cannot be empty", nameof(methodSignatures));

            return JsonConvert.SerializeObject(methodSignatures);
        }

        public MethodInfo ToMethod()
        {
            return ToMethod(AppDomain.CurrentDomain);
        }

        public MethodInfo ToMethod(AppDomain domain)
        {
            Type[] parameterTypes = ParameterTypes.Select(t => t.ToType(domain)).ToArray();
            return DeclaringType.ToType(domain).GetMethod(MethodName, parameterTypes);
        }


        public static event EventHandler<Exception> DeserializingMethodFailedEvent;
    }
}
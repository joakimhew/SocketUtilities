using System;
using System.Collections.Generic;
using System.Linq;

namespace SocketUtilities.Messaging.Extensions
{
    /// <summary>
    /// Extends the <see cref="Type"/>
    /// </summary>
    public static class TypeExtensions
    {

        /// <summary>
        /// Gets the MethodSignatureDto of all methods within a Type. 
        /// <see cref="MethodSignatureDto"/> for more information about the DTO.
        /// </summary>
        /// <param name="type">Type to get MethodSignatures from</param>
        /// <returns>A collection of MethodSignatureDto's</returns>
        public static IEnumerable<MethodSignatureDto> GetAllMethodSignatures(this Type type)
        {
            if(type == null)
                throw new ArgumentNullException(nameof(type), "The provided type cannot be null");

            return type.GetMethods().Select(MethodSignatureDto.FromMethod);
        }

        /// <summary>
        /// Gets the MethodSignatureDto of a specific method within a Type.
        /// <see cref="MethodSignatureDto"/> for more information about the DTO.
        /// </summary>
        /// <param name="type">Type to get MethodSignature from</param>
        /// <param name="methodName">The method within type to get MethodSignatureDto for</param>
        /// <returns></returns>
        public static MethodSignatureDto GetMethodSignature(this Type type, string methodName)
        {
            if(type == null)
                throw new ArgumentNullException(nameof(type), "The provided type cannot be null");

            if(methodName == "")
                throw new ArgumentException("The provided methodName cannot be empty", methodName);

            if(methodName == null)
                throw new ArgumentNullException(nameof(methodName), "The provided methodName cannot be null");

            return MethodSignatureDto.FromMethod(type.GetMethod(methodName));
        }
    }
}
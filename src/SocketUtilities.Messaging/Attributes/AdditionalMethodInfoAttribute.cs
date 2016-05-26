using System;

namespace SocketUtilities.Messaging.Attributes
{
    /// <summary>
    /// Used for futher information about methods
    /// </summary>
    public class AdditionalMethodInfoAttribute : Attribute
    {
        /// <summary>
        /// Used to describe a method
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Used to specify the author of a method
        /// </summary>
        public string Author { get; set; }
    }
}
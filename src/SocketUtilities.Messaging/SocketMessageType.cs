namespace SocketUtilities.Messaging
{
    /// <summary>
    /// Used to specify the type of message for <see cref="SocketMessage"/>
    /// </summary>
    public enum SocketMessageType
    {
        /// <summary>
        /// Used when sending information about methods through the socket. 
        /// Usually in the format of <see cref="Newtonsoft.Json">Json</see>
        /// </summary>
        Methods,
        /// <summary>
        /// Used when sending execution instructions for a method through the socket. 
        /// Usually in the format of <see cref="Newtonsoft.Json">Json</see>
        /// </summary>
        MethodExecution,
        /// <summary>
        /// Used when sending identiy information through the socket. 
        /// </summary>
        Identity,
        /// <summary>
        /// Used when sending normal messages through the socket.
        /// </summary>
        Normal
    }
}
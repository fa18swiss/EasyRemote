namespace EasyRemote.Spec
{
    /// <summary>
    /// Parameters for a protocol of a server
    /// </summary>
    public interface IServerProtocol
    {
        /// <summary>
        /// Port (if not default)
        /// </summary>
        int? Port { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        string Username { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        string Password { get; set; }
        /// <summary>
        /// Protocol
        /// </summary>
        IProtocol Protocol { get; set; }
    }
}
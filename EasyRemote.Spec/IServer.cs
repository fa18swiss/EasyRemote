using System.Collections.Generic;

namespace EasyRemote.Spec
{
    /// <summary>
    /// Server
    /// </summary>
    public interface IServer : IServerBase
    {
        /// <summary>
        /// HostName
        /// </summary>
        string HostName { get; set; }
        /// <summary>
        /// Default username
        /// </summary>
        string DefaultUsername { get; set; }
        /// <summary>
        /// Default password
        /// </summary>
        string DefaultPassword { get; set; }
        /// <summary>
        /// List of protocols of program
        /// </summary>
        IList<IServerProtocol> Protocols { get; }
    }
}
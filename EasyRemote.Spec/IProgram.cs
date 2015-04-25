using System.Collections.Generic;

namespace EasyRemote.Spec
{
   /// <summary>
   /// Program
   /// </summary>
    public interface IProgram
    {
        /// <summary>
        /// Name
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Path
        /// </summary>
        string Path { get; set; }
        /// <summary>
        /// Is activated by user
        /// </summary>
        bool IsActivate { get; set; }
        /// <summary>
        /// Protocols that can be used by program
        /// </summary>
        ICollection<IProtocol> Protocols { get; }
        /// <summary>
        /// Connect to
        /// </summary>
        /// <param name="server">Server</param>
        /// <param name="protocol">With prococol</param>
        /// <returns>Command line args</returns>
        string ConnectTo(IServer server, IServerProtocol protocol);
    }
}
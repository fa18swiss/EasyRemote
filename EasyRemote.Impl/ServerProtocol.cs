using System.ComponentModel;
using EasyRemote.Impl.Converters.JSON;
using EasyRemote.Spec;
using Newtonsoft.Json;

namespace EasyRemote.Impl
{
    /// <summary>
    /// Implementation for a server's protocol
    /// </summary>
    internal class ServerProtocol : IServerProtocol
    {
        /* those annotation are used for the property grid
        // Browsable : determine if property must be showed in grid
        // Category : the grid group property into categories
        // DisplayName : the name that is displayed. Grid use property hard coded name by default
        */ 

        /// <summary>
        /// Port property (int nullable)
        /// </summary>
        [Category("Settings")]
        [Browsable(true)]
        public int? Port { get; set; }

        /// <summary>
        /// Username for authentication
        /// </summary>
        [Category("Credentials")]
        [Browsable(true)]
        public string Username { get; set; }

        /// <summary>
        /// Password for authentication
        /// </summary>
        [Category("Credentials")]
        [Browsable(true)]
        public string Password { get; set; }

        /// <summary>
        /// Protocol
        /// </summary>
        [Browsable(false)]
        [JsonConverter(typeof(ProtocolConverter))]
        public IProtocol Protocol { get; set; }
    }
}
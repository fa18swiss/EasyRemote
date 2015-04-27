using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using EasyRemote.Impl.Converters.JSON;
using EasyRemote.Spec;
using Microsoft.Practices.ObjectBuilder2;
using Newtonsoft.Json;

namespace EasyRemote.Impl
{
    /// <summary>
    /// Implementation for a server
    /// </summary>
    public class Server : IServer
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public Server()
        {
            Protocols = new ObservableCollection<IServerProtocol>();
        }

        /// <summary>
        /// Constructor with all parameters
        /// </summary>
        /// <param name="name">Server's name</param>
        /// <param name="hostName">Server's hostname</param>
        /// <param name="defaultUsername">Default username for authentication</param>
        /// <param name="defaultPassword">Default password for authentication</param>
        /// <param name="protocols">List of protocols to connect with to server</param>
        public Server(string name, string hostName, string defaultUsername, string defaultPassword, params IServerProtocol[] protocols)
            : this()
        {
            Name = name;
            HostName = hostName;
            DefaultUsername = defaultUsername;
            DefaultPassword = defaultPassword;
            protocols.ForEach(p => Protocols.Add(p));
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="helper"></param>
        public Server(IServer helper) : this()
        {
            Name = helper.Name;
            HostName = helper.HostName;
            DefaultUsername = helper.DefaultUsername;
            DefaultPassword = helper.DefaultPassword;
            Protocols = helper.Protocols;
        }

        /* those annotation are used for the property grid
        // Browsable : determine if property must be showed in grid
        // Category : the grid group property into categories
        // DisplayName : the name that is displayed. Grid use property hard coded name by default
        */ 

        /// <summary>
        /// Hostname of the server
        /// </summary>
        [Browsable(true)]
        [Category("Settings")]
        [DisplayName("Host name")]
        public string HostName { get; set; }

        /// <summary>
        /// Default username
        /// </summary>
        [Browsable(true)]
        [Category("Credentials")]
        public string DefaultUsername { get; set; }

        /// <summary>
        /// Default password
        /// </summary>
        [Browsable(true)]
        [Category("Credentials")]
        public string DefaultPassword { get; set; }

        /// <summary>
        /// List of protocole to connect with
        /// </summary>
        [Browsable(false)]
        [JsonConverter(typeof(GenericListConverter<ServerProtocol, IServerProtocol>))]
        public IList<IServerProtocol> Protocols { get; private set; }

        /// <summary>
        /// Server name
        /// </summary>
        [Browsable(true)]
        [Category("Settings")]
        public string Name { get; set; }

        /// <summary>
        /// Type used for JSON deserialization
        /// </summary>
        [Browsable(false)]
        public ServerClassType Type {
            get { return ServerClassType.Server; }
        }
    }
}
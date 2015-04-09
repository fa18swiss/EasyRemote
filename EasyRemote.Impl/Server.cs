using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using EasyRemote.Impl.Converters.JSON;
using EasyRemote.Spec;
using Microsoft.Practices.ObjectBuilder2;
using Newtonsoft.Json;

namespace EasyRemote.Impl
{
    public class Server : IServer
    {
        public Server()
        {
            Protocols = new ObservableCollection<IServerProtocol>();
        }
        public Server(string hostName, string defaultUsername, string defaultPassword, params IServerProtocol[] protocols)
            : this()
        {
            HostName = hostName;
            DefaultUsername = defaultUsername;
            DefaultPassword = defaultPassword;
            protocols.ForEach(p => Protocols.Add(p));
        }

        public Server(ServerBaseHelper helper) : this()
        {
            HostName = helper.HostName;
            DefaultUsername = helper.DefaultUsername;
            DefaultPassword = helper.DefaultPassword;
            Protocols = helper.Protocols;
        }

        [Browsable(true)]
        [Category("Settings")]
        [DisplayName("Host name")]
        public string HostName { get; set; }

        [Browsable(true)]
        [Category("Credentials")]
        public string DefaultUsername { get; set; }

        [Browsable(true)]
        [Category("Credentials")]
        public string DefaultPassword { get; set; }

        [Browsable(false)]
        [JsonConverter(typeof(GenericListConverter<ServerProtocol, IServerProtocol>))]
        public IList<IServerProtocol> Protocols { get; private set; }

        [Browsable(true)]
        [Category("Settings")]
        public string Name { get; set; }

        public ServerClassType Type {
            get { return ServerClassType.Server; }
        }
    }
}
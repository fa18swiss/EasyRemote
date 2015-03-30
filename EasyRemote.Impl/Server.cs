using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using EasyRemote.Spec;
using Microsoft.Practices.ObjectBuilder2;

namespace EasyRemote.Impl
{
    internal class Server : IServer
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

        [Browsable(true)]
        [Category("Settings")]
        [DisplayName("Host name")]
        public string HostName { get; set; }
        public string DefaultUsername { get; set; }
        public string DefaultPassword { get; set; }

        [Browsable(false)]
        public IList<IServerProtocol> Protocols { get; private set; }

        [Browsable(true)]
        [Category("Settings")]
        public string Name { get; set; }
    }
}
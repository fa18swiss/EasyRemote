using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Server(string name, string hostName, string defaultUsername, string defaultPassword, params IServerProtocol[] protocols)
            : this()
        {
            Name = name;
            HostName = hostName;
            DefaultUsername = defaultUsername;
            DefaultPassword = defaultPassword;
            protocols.ForEach(p => Protocols.Add(p));
        }

        public string HostName { get; set; }
        public string DefaultUsername { get; set; }
        public string DefaultPassword { get; set; }
        public IList<IServerProtocol> Protocols { get; private set; }
        public string Name { get; set; }
    }
}
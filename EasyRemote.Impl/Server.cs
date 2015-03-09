using System.Collections.Generic;
using System.Collections.ObjectModel;
using EasyRemote.Spec;

namespace EasyRemote.Impl
{
    internal class Server : IServer
    {
        public Server()
        {
            Protocols = new ObservableCollection<IServerProtocol>();
        }

        public string HostName { get; set; }
        public IList<IServerProtocol> Protocols { get; private set; }
        public string Name { get; set; }
    }
}
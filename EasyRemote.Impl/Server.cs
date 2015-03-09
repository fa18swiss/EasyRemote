using System.Collections.Generic;
using EasyRemote.Spec;

namespace EasyRemote.Impl
{
    internal class Server : IServer
    {
        public Server()
        {
            Protocols = new List<IServerProtocol>();
        }

        public string HostName { get; set; }
        public IList<IServerProtocol> Protocols { get; private set; }
        public string Name { get; set; }
    }
}
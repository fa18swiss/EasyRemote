using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyRemote.Spec;

namespace EasyRemote.Impl.Converters.JSON
{
    public class ServerBaseHelper : IServer, IServerGroup
    {
        public string Name { get; set; }
        public ServerClassType Type { get; private set; }
        public string HostName { get; set; }
        public string DefaultUsername { get; set; }
        public string DefaultPassword { get; set; }
        public IList<IServerProtocol> Protocols { get; private set; }
        public IList<IServerBase> Childrens { get; private set; }
    }
}

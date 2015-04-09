using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyRemote.Spec;
using Newtonsoft.Json;

namespace EasyRemote.Impl.Converters.JSON
{
    public class ServerBaseHelper : IServer, IServerGroup
    {
        public string Name { get; set; }
        public ServerClassType Type { get;  set; }
        public string HostName { get; set; }
        public string DefaultUsername { get; set; }
        public string DefaultPassword { get; set; }

        [JsonConverter(typeof(GenericListConverter<ServerProtocol,IServerProtocol>))]
        public IList<IServerProtocol> Protocols { get; set; }

        [JsonConverter(typeof(ServerBaseConverter))]
        public IList<IServerBase> Childrens { get; set; }
    }
}

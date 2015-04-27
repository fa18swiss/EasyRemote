using System.Collections.Generic;
using EasyRemote.Impl.Converters.JSON;
using EasyRemote.Spec;
using Newtonsoft.Json;

namespace EasyRemote.Impl.JSON
{

    /// <summary>
    /// As the serialized configuration list contains objects of different types (implement IServer or IServerGroup)
    /// we use this class for the deserialization. First we deserialize objects in a container that implements both interfaces
    /// and then we determine the real type and create the final object (IServer or IServerGroup) using copy constructors.
    /// </summary>
    public class ServerBaseHelper : IServer, IServerGroup //implements both interface
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

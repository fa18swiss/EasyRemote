using System.Collections.Generic;

namespace EasyRemote.Spec
{
    public interface IServer : IServerBase
    {
        string HostName { get; set; }
        string DefaultUsername { get; set; }
        string DefaultPassword { get; set; }
        IList<IServerProtocol> Protocols { get; }
    }
}
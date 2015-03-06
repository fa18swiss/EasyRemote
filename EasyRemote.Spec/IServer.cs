using System.Collections.Generic;

namespace EasyRemote.Spec
{
    public interface IServer : IServerBase
    {
        string HostName { get; set; }
        IList<IServerProtocol> Protocols { get; }
    }
}
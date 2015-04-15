using System.Collections.Generic;

namespace EasyRemote.Spec
{
    public interface IProgram
    {
        string Name { get; set; }
        string Path { get; set; }
        bool IsActivate { get; set; }
        IList<IProtocol> Protocols { get; }

        string ConnectTo(IServer server, IServerProtocol protocol);
    }
}
using System.Collections.Generic;

namespace EasyRemote.Spec
{
    public interface IProgram
    {
        string Name { get; set; }
        string Path { get; set; }
        IList<IProtocol> Protocols { get; }

        void ConnectTo(IServer server, IServerProtocol protocol);
    }
}
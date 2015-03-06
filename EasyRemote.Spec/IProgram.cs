using System.Collections.Generic;

namespace EasyRemote.Spec
{
    public interface IProgram
    {
        IList<IProtocol> Porotocols { get; set; }

        void ConnectTo(IServer server, IProtocol protocol);
    }
}
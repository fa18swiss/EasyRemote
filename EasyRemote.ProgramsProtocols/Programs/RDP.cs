using System;
using EasyRemote.ProgramsProtocols.Protocols;
using EasyRemote.Spec;

namespace EasyRemote.ProgramsProtocols.Programs
{
    class RDP : _Base
    {
        public RDP(Protocols.RDP rdp)
            : base("RDP", rdp)
        {
            
        }

        public override void ConnectTo(IServer server, IServerProtocol protocol)
        {
            throw new NotImplementedException();
        }
    }
}

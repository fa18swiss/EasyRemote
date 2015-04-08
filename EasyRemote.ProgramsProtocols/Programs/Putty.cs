using System;
using EasyRemote.ProgramsProtocols.Protocols;
using EasyRemote.Spec;

namespace EasyRemote.ProgramsProtocols.Programs
{
    class Putty : _Base
    {
        public Putty(SSH ssh, Telnet telnet)
            : base("Putty", ssh, telnet)
        {
            
        }

        public override void ConnectTo(IServer server, IServerProtocol protocol)
        {
            throw new NotImplementedException();
        }
    }
}

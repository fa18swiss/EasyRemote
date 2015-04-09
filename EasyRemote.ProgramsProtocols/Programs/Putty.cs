using System;
using EasyRemote.ProgramsProtocols.Protocols;
using EasyRemote.Spec;

namespace EasyRemote.ProgramsProtocols.Programs
{
    class Putty : _Base
    {
        public Putty(SSH ssh, Protocols.Telnet telnet, Serial serial)
            : base("Putty", ssh, telnet, serial)
        {
            
        }

        public override string ConnectTo(IServer server, IServerProtocol protocol)
        {
            throw new NotImplementedException();
        }
    }
}

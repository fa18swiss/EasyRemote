using System;
using EasyRemote.ProgramsProtocols.Protocols;
using EasyRemote.Spec;

namespace EasyRemote.ProgramsProtocols.Programs
{
    class WinSCP : _Base
    {
        public WinSCP(SSH ssh, FTP ftp)
            : base("WinSCP", ssh, ftp)
        {
            
        }

        public override void ConnectTo(IServer server, IServerProtocol protocol)
        {
            throw new NotImplementedException();
        }
    }
}

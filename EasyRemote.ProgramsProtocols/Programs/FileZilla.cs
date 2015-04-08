using System;
using EasyRemote.ProgramsProtocols.Protocols;
using EasyRemote.Spec;

namespace EasyRemote.ProgramsProtocols.Programs
{
    class FileZilla : _Base
    {
        public FileZilla(SSH ssh, FTP ftp, FTPS ftps)
            : base("FileZilla", ssh, ftp, ftps)
        {
            
        }

        public override string ConnectTo(IServer server, IServerProtocol protocol)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using EasyRemote.ProgramsProtocols.Protocols;
using EasyRemote.Spec;

namespace EasyRemote.ProgramsProtocols.Programs
{
    class TurboVNC : _Base
    {
        public TurboVNC(VNC vnc)
            : base("TurboVNC", vnc)
        {
            
        }

        public override void ConnectTo(IServer server, IServerProtocol protocol)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Text;
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

        public override string ConnectTo(IServer server, IServerProtocol protocol)
        {
            var builder = new StringBuilder();
            builder.Append("/fullscreen /v:");
            builder.Append(server.HostName);
            if (protocol.Port.HasValue)
            {
                builder.Append(':');
                builder.Append(protocol.Port.Value);
            }
            return builder.ToString();
        }
    }
}

using System.Text;
using EasyRemote.Spec;

namespace EasyRemote.ProgramsProtocols.Programs
{
    internal class Telnet : _Base
    {
        public Telnet(Protocols.Telnet telnet)
            : base("Telnet", telnet)
        {
        }

        public override string ConnectTo(IServer server, IServerProtocol protocol)
        {
            var builder = new StringBuilder();

            builder.Append(server.HostName);

            if (protocol.Port != null)
            {
                builder.Append(' ');
                builder.Append(protocol.Port.Value);
            }

            return builder.ToString();
        }
    }
}
using System.Text;
using EasyRemote.Impl.Extension;
using EasyRemote.ProgramsProtocols.Protocols;
using EasyRemote.Spec;

namespace EasyRemote.ProgramsProtocols.Programs
{
    internal class TurboVNC : _Base
    {
        public TurboVNC(VNC vnc)
            : base("TurboVNC", vnc)
        {
        }

        public override string ConnectTo(IServer server, IServerProtocol protocol)
        {
            var builder = new StringBuilder();

            builder.Append(server.HostName);
            if (protocol.Port.HasValue)
            {
                builder.Append("::");
                builder.Append(protocol.Port.Value);
            }
            var pwd = server.GetPasswordForProtocol(protocol);
            if (!string.IsNullOrEmpty(pwd))
            {
                builder.Append(" /password ");
                builder.Append(pwd);
            }
            var user = server.GetUsernameForProtocol(protocol);
            if (!string.IsNullOrEmpty(user))
            {
                builder.Append(" /user ");
                builder.Append(user);
            }
            return builder.ToString();
        }
    }
}
using System.Collections.Generic;
using System.Text;
using EasyRemote.Impl.Extension;
using EasyRemote.ProgramsProtocols.Protocols;
using EasyRemote.Spec;

namespace EasyRemote.ProgramsProtocols.Programs
{
    internal class WinSCP : _Base
    {
        private readonly IDictionary<IProtocol, string> protocolsDictionnary;

        public WinSCP(SSH ssh, FTP ftp, FTPS ftps)
            : base("WinSCP", ssh, ftp, ftps)
        {
            protocolsDictionnary = new Dictionary<IProtocol, string>();
            protocolsDictionnary.Add(ssh, "scp");
            protocolsDictionnary.Add(ftp, "ftp");
            protocolsDictionnary.Add(ftps, "sftp");
        }

        public override string ConnectTo(IServer server, IServerProtocol protocol)
        {
            var builder = new StringBuilder();
            builder.Append(protocolsDictionnary[protocol.Protocol]);
            builder.Append("://");

            var user = server.GetUsernameForProtocol(protocol);
            if (!string.IsNullOrEmpty(user))
            {
                builder.Append(user);
                var pwd = server.GetPasswordForProtocol(protocol);
                if (!string.IsNullOrEmpty(pwd))
                {
                    builder.Append(':');
                    builder.Append(pwd);
                }
                builder.Append('@');
            }
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
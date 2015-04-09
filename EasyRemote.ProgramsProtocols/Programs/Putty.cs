using System.Diagnostics;
using System.Text;
using EasyRemote.Impl.Extension;
using EasyRemote.ProgramsProtocols.Protocols;
using EasyRemote.Spec;
using Microsoft.Win32;

namespace EasyRemote.ProgramsProtocols.Programs
{
    internal class Putty : _Base
    {
        private readonly SSH ssh;
        private readonly Protocols.Telnet telnet;
        private readonly Serial serial;

        public Putty(SSH ssh, Protocols.Telnet telnet, Serial serial)
            : base("Putty", ssh, telnet, serial)
        {
            this.ssh = ssh;
            this.telnet = telnet;
            this.serial = serial;
        }

        public override string ConnectTo(IServer server, IServerProtocol protocol)
        {
            var builder = new StringBuilder();
            if (ssh.Equals(protocol.Protocol) || telnet.Equals(protocol.Protocol))
            {
                builder.Append('-');
                builder.Append(protocol.Protocol.Name.ToLower());
                builder.Append(' ');
                var user = server.GetUsernameForProtocol(protocol);
                if (!string.IsNullOrEmpty(user))
                {
                    builder.Append(user);
                    builder.Append('@');
                }
                builder.Append(server.HostName);
                if (protocol.Port.HasValue)
                {
                    builder.Append(" -P ");
                    builder.Append(protocol.Port.Value);
                }
                var pwd = server.GetPasswordForProtocol(protocol);
                if (!string.IsNullOrEmpty(pwd) && ssh.Equals(protocol.Protocol))
                {
                    builder.Append(" -pw ");
                    builder.Append(pwd);
                }
            }
            else if (serial.Equals(protocol.Protocol))
            {
                builder.Append("-serial ");
                builder.Append(server.HostName);
                if (protocol.Port.HasValue)
                {
                    builder.Append(" -sercfg ");
                    builder.Append(protocol.Port);
                }
            }
            return builder.ToString();
        }
    }
}
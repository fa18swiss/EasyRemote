using System.Text;
using EasyRemote.ProgramsProtocols.Protocols;
using EasyRemote.Spec;

namespace EasyRemote.ProgramsProtocols.Programs
{
    internal abstract class _HttpClient : _Base
    {
        public _HttpClient(string name, HTTP http, HTTPS https) : base(name, http, https)
        {
        }

        public override string ConnectTo(IServer server, IServerProtocol protocol)
        {
            var builder = new StringBuilder();

            builder.Append(protocol.Protocol.Name.ToLower());
            builder.Append("://");
            builder.Append(server.HostName);
            if (protocol.Port.HasValue)
            {
                builder.Append(':');
                builder.Append(protocol.Port.Value);
            }
            builder.Append('/');
            
            return builder.ToString();
        }
    }
}

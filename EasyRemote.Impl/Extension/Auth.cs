using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EasyRemote.Spec;

namespace EasyRemote.Impl.Extension
{
    public static class Auth
    {
        public static string GetUsernameForProtocol(this IServer server, IServerProtocol protocol)
        {
            if (!string.IsNullOrEmpty(protocol.Username))
            {
                return protocol.Username;
            }
            if (!string.IsNullOrEmpty(server.DefaultUsername))
            {
                return server.DefaultUsername;
            }
            return null;
        }
        public static string GetPasswordForProtocol(this IServer server, IServerProtocol protocol)
        {
            if (!string.IsNullOrEmpty(protocol.Password))
            {
                return protocol.Password;
            }
            if (!string.IsNullOrEmpty(server.DefaultPassword))
            {
                return server.DefaultPassword;
            }
            return null;
        }
    }
}

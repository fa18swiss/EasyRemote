using EasyRemote.Spec;

namespace EasyRemote.Impl.Extension
{
    public static class Auth
    {
        /// <summary>
        /// GetUsernameForProtocol
        /// </summary>
        /// <param name="server">Server</param>
        /// <param name="protocol">Protocol</param>
        /// <returns>Username</returns>
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
        /// <summary>
        /// GetPasswordForProtocol
        /// </summary>
        /// <param name="server">Server</param>
        /// <param name="protocol">Protocol</param>
        /// <returns>Password</returns>
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

using EasyRemote.Spec;

namespace EasyRemote.Impl.Extension
{
    public static class Port
    {
        public static int GetPort(this IServerProtocol protocol)
        {
            return protocol.Port ?? protocol.Protocol.DefaultPort;
        }
    }
}
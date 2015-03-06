using EasyRemote.Spec;

namespace EasyRemote.Impl
{
    internal class ServerProtocol : IServerProtocol
    {
        public int? Port { get; set; }
        public IProtocol Protocol { get; set; }
    }
}
using EasyRemote.Spec;

namespace EasyRemote.Impl
{
    internal class ServerProtocol : IServerProtocol
    {
        public int? Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IProtocol Protocol { get; set; }
    }
}
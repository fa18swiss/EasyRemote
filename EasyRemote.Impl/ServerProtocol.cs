using System.ComponentModel;
using EasyRemote.Spec;

namespace EasyRemote.Impl
{
    internal class ServerProtocol : IServerProtocol
    {
        [Category("Settings")]
        [Browsable(true)]
        public int? Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [Browsable(false)]
        public IProtocol Protocol { get; set; }
    }
}
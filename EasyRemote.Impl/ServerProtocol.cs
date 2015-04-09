using System.ComponentModel;
using EasyRemote.Impl.Converters.JSON;
using EasyRemote.Spec;
using Newtonsoft.Json;

namespace EasyRemote.Impl
{
    internal class ServerProtocol : IServerProtocol
    {
        [Category("Settings")]
        [Browsable(true)]
        public int? Port { get; set; }

        [Category("Credentials")]
        [Browsable(true)]
        public string Username { get; set; }

        [Category("Credentials")]
        [Browsable(true)]
        public string Password { get; set; }

        [Browsable(false)]
        [JsonConverter(typeof(GenericConverter<Protocol>))]
        public IProtocol Protocol { get; set; }
    }
}
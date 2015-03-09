using EasyRemote.Spec;

namespace EasyRemote.Impl
{
    internal class Protocol : IProtocol
    {
        public string Name { get; set; }
        public int DefaultPort { get; set; }
    }
}
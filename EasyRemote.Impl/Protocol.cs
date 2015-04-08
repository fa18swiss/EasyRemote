using System;
using EasyRemote.Spec;

namespace EasyRemote.Impl
{
    [Obsolete]
    internal class Protocol : IProtocol
    {
        public string Name { get; set; }
        public int DefaultPort { get; set; }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var protocol = obj as IProtocol;
            return protocol != null && string.Equals(Name, protocol.Name,StringComparison.CurrentCultureIgnoreCase);
        }
        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}
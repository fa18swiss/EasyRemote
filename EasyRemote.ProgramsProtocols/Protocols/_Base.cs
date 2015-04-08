using System;
using EasyRemote.Spec;

namespace EasyRemote.ProgramsProtocols.Protocols
{
    internal abstract class _Base : IProtocol
    {
        private readonly string name;
        private readonly int port;

        protected _Base(String name, int port)
        {
            this.name = name;
            this.port = port;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as IProtocol;
            return other != null && String.Equals(Name, other.Name);
        }

        public string Name { 
            get { return name; }
            set {} 
        }
        public int DefaultPort
        {
            get { return port; }
            set { }
        }
    }
}

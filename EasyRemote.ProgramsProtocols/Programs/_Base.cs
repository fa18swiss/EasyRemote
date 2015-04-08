using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EasyRemote.Spec;

namespace EasyRemote.ProgramsProtocols.Programs
{
    internal abstract class _Base : IProgram
    {
        private readonly string name;
        private readonly IList<IProtocol> protocols; 
        protected _Base(string name, params IProtocol[] protocols)
        {
            this.name = name;
            this.protocols = new ReadOnlyCollection<IProtocol>(protocols);
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as IProgram;
            return other != null && String.Equals(Name, other.Name);
        }

        public string Name {
            get { return name; }
            set{}
        }
        public string Path { get; set; }

        public IList<IProtocol> Protocols
        {
            get { return protocols; }
        }
        public abstract string ConnectTo(IServer server, IServerProtocol protocol);
    }
}

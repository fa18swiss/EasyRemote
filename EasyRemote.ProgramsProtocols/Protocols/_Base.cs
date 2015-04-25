using System;
using EasyRemote.Spec;

namespace EasyRemote.ProgramsProtocols.Protocols
{
    /// <summary>
    /// Default implementaiton for protool
    /// </summary>
    internal abstract class _Base : IProtocol
    {
        private readonly string name;

        protected _Base(String name, int defaultPort)
        {
            this.name = name;
            DefaultPort = defaultPort;
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

        public string Name
        {
            get { return name; }
        }

        public int DefaultPort { get; set; }
    }
}
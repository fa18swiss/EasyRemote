using System.Collections.Generic;
using System.Linq;
using EasyRemote.Spec;
using Microsoft.Practices.Unity;

namespace EasyRemote.Impl
{
    internal class ProgramsProtocolsList : IProgramsProtocolsList
    {
        private readonly ICollection<IProgram> programs;
        private readonly ICollection<IProtocol> protocols;

        public ProgramsProtocolsList(IUnityContainer container)
        {
            programs = container.ResolveAll<IProgram>().ToList();
            protocols = container.ResolveAll<IProtocol>().ToList();
        }
        public ICollection<IProgram> Programs { get { return programs; } }
        public ICollection<IProtocol> Protocols { get { return protocols; } }
    }
}

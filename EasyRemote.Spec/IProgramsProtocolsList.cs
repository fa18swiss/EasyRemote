using System.Collections.Generic;

namespace EasyRemote.Spec
{
    public interface IProgramsProtocolsList
    {
        ICollection<IProgram> Programs { get; }
        ICollection<IProtocol> Protocols { get; }
    }
}

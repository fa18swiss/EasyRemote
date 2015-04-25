using System.Collections.Generic;

namespace EasyRemote.Spec
{
    /// <summary>
    /// List of programs and prococols of EasyRemote
    /// </summary>
    public interface IProgramsProtocolsList
    {
        /// <summary>
        /// Programs
        /// </summary>
        ICollection<IProgram> Programs { get; }
        /// <summary>
        /// Prococols
        /// </summary>
        ICollection<IProtocol> Protocols { get; }
    }
}

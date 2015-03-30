using System.Collections.Generic;

namespace EasyRemote.Spec
{
    public interface IConfig
    {
        IList<IProgram> Programs { get; }
        IList<IProtocol> Protocols { get; }
        IServerGroup RootGroup { get; set; }

        void Load(string path);
        void Save(string path);
    }
}
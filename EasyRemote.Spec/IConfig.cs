using System.Collections.Generic;

namespace EasyRemote.Spec
{
    public interface IConfig
    {
        IList<IProgram> Programs { get; }
        IServerGroup RootGroup { get; set; }

        void Load(string path);
        void Save(string path);
    }
}
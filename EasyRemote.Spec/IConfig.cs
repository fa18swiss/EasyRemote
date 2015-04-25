using System.Collections.Generic;

namespace EasyRemote.Spec
{
    /// <summary>
    /// Config serialized
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// List of programs (with path)
        /// </summary>
        IList<IProgram> Programs { get; }
        /// <summary>
        /// Root group of tree
        /// </summary>
        IServerGroup RootGroup { get; set; }

        void Load(string path);
        void Save(string path);
    }
}
using System;
using System.Collections.Generic;
using EasyRemote.Spec;

namespace EasyRemote.Impl
{
    internal class Config : IConfig
    {
        public IList<IProgram> Programs { get; set; }
        public IServerGroup RootGroup { get; set; }

        public void Load(string path)
        {
            throw new NotImplementedException();
        }

        public void Save(string path)
        {
            throw new NotImplementedException();
        }
    }
}
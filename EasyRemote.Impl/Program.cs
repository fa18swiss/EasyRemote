using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EasyRemote.Spec;
using Microsoft.Practices.ObjectBuilder2;

namespace EasyRemote.Impl
{
    internal class Program : IProgram
    {
        public Program()
        {
            Protocols = new ObservableCollection<IProtocol>();
        }
        public Program(string name,string path, params IProtocol[] protocols)
            : this()
        {
            Name = name;
            Path = path;
            protocols.ForEach(p => Protocols.Add(p));
        }

        public string Name { get; set; }
        public string Path { get; set; }
        public IList<IProtocol> Protocols { get; private set; }

        public void ConnectTo(IServer server, IServerProtocol protocol)
        {
            throw new NotImplementedException();
        }
    }
}
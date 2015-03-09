using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EasyRemote.Spec;

namespace EasyRemote.Impl
{
    internal class Program : IProgram
    {
        public Program()
        {
            Protocols = new ObservableCollection<IProtocol>();
        }

        public string Name { get; set; }
        public IList<IProtocol> Protocols { get; private set; }

        public void ConnectTo(IServer server, IProtocol protocol)
        {
            throw new NotImplementedException();
        }
    }
}
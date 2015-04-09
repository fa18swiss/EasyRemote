using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EasyRemote.Impl.Converters.JSON;
using EasyRemote.Spec;
using Microsoft.Practices.ObjectBuilder2;
using Newtonsoft.Json;

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

        [JsonConverter(typeof(GenericListConverter<Protocol, IProtocol>))]
        public IList<IProtocol> Protocols { get; private set; }

        public void ConnectTo(IServer server, IServerProtocol protocol)
        {
            throw new NotImplementedException();
        }
    }
}
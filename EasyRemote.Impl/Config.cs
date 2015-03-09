using System;
using System.Collections.Generic;
using EasyRemote.Spec;

namespace EasyRemote.Impl
{
    internal class Config : IConfig
    {
        public Config()
        {
            Programs = new List<IProgram>();
            RootGroup = new ServerGroup();
            RootGroup.Childrens.Add(new Server
            {
                HostName = "toot",
                Name = "dsafsadf",
            });
            var g = new ServerGroup
            {
                Name = "groa dsafsadf",
            };
            g.Childrens.Add(new Server
            {
                Name= "ch2",
            });
            RootGroup.Childrens.Add(g);
            var s = new Server
            {
                Name = "toto"
            };
            s.Protocols.Add(new ServerProtocol
            {
                Protocol = new Protocol
                {
                    Name = "SSH",
                    DefaultPort = 22,
                }
            });
            s.Protocols.Add(new ServerProtocol
            {
                Protocol = new Protocol
                {
                    Name = "VNC",
                    DefaultPort = 1234,
                }
            });
            RootGroup.Childrens.Add(s);
        }
        public IList<IProgram> Programs { get; private set; }
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
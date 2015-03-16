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
            var vnc = new Protocol
            {
                Name = "VNC",
                DefaultPort = 1234,
            };
            var ssh = new Protocol
            {
                Name = "SSH",
                DefaultPort = 22,
            };
            var ftp = new Protocol
            {
                Name = "FTP",
                DefaultPort = 21,
            };
            RootGroup = new ServerGroup();
            RootGroup.Childrens.Add(new Server
            {
                HostName = "toot",
                Name = "dsafsadf",
            });
            RootGroup.Childrens.Add(new ServerGroup("groa dsafsadf",
                new Server
                {
                    Name = "ch2",
                }));
            RootGroup.Childrens.Add(new Server("toto", null, null,
                new ServerProtocol
                {
                    Protocol = ssh,
                    Port = 5000,
                },
                new ServerProtocol
                {
                    Protocol = vnc
                }));
            Programs.Add(new Program("Putty", @"C:\Program Files (x86)\mRemoteNG\PuTTYNG.exe", ssh));
            Programs.Add(new Program("FileZilla", @"C:\Program Files (x86)\FileZilla FTP Client\filezilla.exe", ftp, ssh));
            Programs.Add(new Program("WinSCP", @"C:\Program Files (x86)\WinSCP\WinSCP.exe", ssh));
            Programs.Add(new Program("TurboVNC", @"C:\Program Files\TurboVNC\vncviewer.exe", vnc));
            Programs.Add(new Program("test", @"", vnc));
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
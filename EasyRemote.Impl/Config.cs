using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using EasyRemote.Spec;
using Microsoft.Practices.Unity;

namespace EasyRemote.Impl
{
    internal class Config : IConfig
    {
        public Config(IUnityContainer container)
        {
            Programs = new ReadOnlyCollection<IProgram>(container.ResolveAll<IProgram>().ToList());
            Protocols = new ReadOnlyCollection<IProtocol>(container.ResolveAll<IProtocol>().ToList());
            RootGroup = new ServerGroup();

            var vnc = Protocols.FirstOrDefault(p => p.Name.Equals("VNC"));
            var ssh = Protocols.FirstOrDefault(p => p.Name.Equals("SSH"));
            var ftp = Protocols.FirstOrDefault(p => p.Name.Equals("FTP"));
            var http = Protocols.FirstOrDefault(p => p.Name.Equals("HTTP"));
            RootGroup.Childrens.Add(new Server ("toot","dsafsadf",null, new ServerProtocol
            {
                Protocol = ftp
            }));
            RootGroup.Childrens.Add(new ServerGroup("groa dsafsadf",
                new Server("srv2","srv2", "null", new ServerProtocol
                {
                    Protocol = http
                })));
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
            Programs.FirstOrDefault(p => p.Name.Equals("Putty")).Path = @"C:\Program Files (x86)\mRemoteNG\PuTTYNG.exe";
            Programs.FirstOrDefault(p => p.Name.Equals("FileZilla")).Path = @"C:\Program Files (x86)\FileZilla FTP Client\filezilla.exe";
            Programs.FirstOrDefault(p => p.Name.Equals("WinSCP")).Path = @"C:\Program Files (x86)\WinSCP\WinSCP.exe";
            Programs.FirstOrDefault(p => p.Name.Equals("TurboVNC")).Path = @"C:\Program Files\TurboVNC\vncviewer.exe";
           
        }

        public IList<IProgram> Programs { get; private set; }
        public IList<IProtocol> Protocols { get; private set; }
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
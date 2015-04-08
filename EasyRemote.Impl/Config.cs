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
            var rdp = Protocols.FirstOrDefault(p => p.Name.Equals("RDP"));
            RootGroup.Childrens.Add(new Server ("toot","dsafsadf",null,null, new ServerProtocol
            {
                Protocol = ftp
            }));
            RootGroup.Childrens.Add(new ServerGroup("groa dsafsadf",
                new Server("srv2","srv2", null,null, new ServerProtocol
                {
                    Protocol = http
                },
                new ServerProtocol
                {
                    Protocol = rdp
                })));
            RootGroup.Childrens.Add(new Server("cuda1", "157.26.103.175", null, null,
                new ServerProtocol
                {
                    Protocol = ssh,
                },
                new ServerProtocol
                {
                    Protocol = vnc,
                    Port = 5910,
                }));
            Programs.FirstOrDefault(p => p.Name.Equals("Putty")).Path = @"%ProgramFiles(x86)%\mRemoteNG\PuTTYNG.exe";
            Programs.FirstOrDefault(p => p.Name.Equals("FileZilla")).Path = @"%ProgramFiles(x86)%\FileZilla FTP Client\filezilla.exe";
            Programs.FirstOrDefault(p => p.Name.Equals("WinSCP")).Path = @"%ProgramFiles(x86)%\WinSCP\WinSCP.exe";
            Programs.FirstOrDefault(p => p.Name.Equals("TurboVNC")).Path = @"C:\Program Files\TurboVNC\vncviewer.exe";
            Programs.FirstOrDefault(p => p.Name.Equals("Firefox")).Path = @"%ProgramFiles(x86)%\Mozilla Firefox\firefox.exe";
            Programs.FirstOrDefault(p => p.Name.Equals("Chrome")).Path = @"%ProgramFiles(x86)%\Google\Chrome\Application\chrome.exe";
            Programs.FirstOrDefault(p => p.Name.Equals("InternetExplorer")).Path = @"%ProgramFiles(x86)%\Internet Explorer\iexplore.exe";
            Programs.FirstOrDefault(p => p.Name.Equals("RDP")).Path = @"%windir%\system32\mstsc.exe";
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
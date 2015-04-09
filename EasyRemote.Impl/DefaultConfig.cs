using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using EasyRemote.Impl.Converters.JSON;
using EasyRemote.Spec;
using Newtonsoft.Json;
using System.Linq;

namespace EasyRemote.Impl
{
    internal class DefaultConfig : Config
    {
        public DefaultConfig(IProgramsProtocolsList programsProtocols)
        {
            RootGroup = new ServerGroup();
            Programs = new ReadOnlyCollection<IProgram>(programsProtocols.Programs.ToList());
  
            var vnc = programsProtocols.Protocols.FirstOrDefault(p => p.Name.Equals("VNC"));
            var ssh = programsProtocols.Protocols.FirstOrDefault(p => p.Name.Equals("SSH"));
            var ftp = programsProtocols.Protocols.FirstOrDefault(p => p.Name.Equals("FTP"));
            var http = programsProtocols.Protocols.FirstOrDefault(p => p.Name.Equals("HTTP"));
            var rdp = programsProtocols.Protocols.FirstOrDefault(p => p.Name.Equals("RDP"));
            var telnet = programsProtocols.Protocols.FirstOrDefault(p => p.Name.Equals("Telnet"));
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
                },
                new ServerProtocol
                {
                    Protocol = telnet
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
            programsProtocols.Programs.FirstOrDefault(p => p.Name.Equals("Putty")).Path = @"%ProgramFiles(x86)%\mRemoteNG\PuTTYNG.exe";
            programsProtocols.Programs.FirstOrDefault(p => p.Name.Equals("FileZilla")).Path = @"%ProgramFiles(x86)%\FileZilla FTP Client\filezilla.exe";
            programsProtocols.Programs.FirstOrDefault(p => p.Name.Equals("WinSCP")).Path = @"%ProgramFiles(x86)%\WinSCP\WinSCP.exe";
            programsProtocols.Programs.FirstOrDefault(p => p.Name.Equals("TurboVNC")).Path = @"C:\Program Files\TurboVNC\vncviewer.exe";
            programsProtocols.Programs.FirstOrDefault(p => p.Name.Equals("Firefox")).Path = @"%ProgramFiles(x86)%\Mozilla Firefox\firefox.exe";
            programsProtocols.Programs.FirstOrDefault(p => p.Name.Equals("Chrome")).Path = @"%ProgramFiles(x86)%\Google\Chrome\Application\chrome.exe";
            programsProtocols.Programs.FirstOrDefault(p => p.Name.Equals("InternetExplorer")).Path = @"%ProgramFiles(x86)%\Internet Explorer\iexplore.exe";
            programsProtocols.Programs.FirstOrDefault(p => p.Name.Equals("RDP")).Path = @"%windir%\System32\mstsc.exe";
            programsProtocols.Programs.FirstOrDefault(p => p.Name.Equals("Telnet")).Path = @"%windir%\sysnative\telnet.exe";
        }

        
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using EasyRemote.Impl.Converters.JSON;
using EasyRemote.Spec;
using Newtonsoft.Json;
using System.Windows;
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
            var telnet = Protocols.FirstOrDefault(p => p.Name.Equals("Telnet"));
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
            Programs.FirstOrDefault(p => p.Name.Equals("Putty")).Path = @"%ProgramFiles(x86)%\mRemoteNG\PuTTYNG.exe";
            Programs.FirstOrDefault(p => p.Name.Equals("FileZilla")).Path = @"%ProgramFiles(x86)%\FileZilla FTP Client\filezilla.exe";
            Programs.FirstOrDefault(p => p.Name.Equals("WinSCP")).Path = @"%ProgramFiles(x86)%\WinSCP\WinSCP.exe";
            Programs.FirstOrDefault(p => p.Name.Equals("TurboVNC")).Path = @"C:\Program Files\TurboVNC\vncviewer.exe";
            Programs.FirstOrDefault(p => p.Name.Equals("Firefox")).Path = @"%ProgramFiles(x86)%\Mozilla Firefox\firefox.exe";
            Programs.FirstOrDefault(p => p.Name.Equals("Chrome")).Path = @"%ProgramFiles(x86)%\Google\Chrome\Application\chrome.exe";
            Programs.FirstOrDefault(p => p.Name.Equals("InternetExplorer")).Path = @"%ProgramFiles(x86)%\Internet Explorer\iexplore.exe";
            Programs.FirstOrDefault(p => p.Name.Equals("RDP")).Path = @"%windir%\System32\mstsc.exe";
            Programs.FirstOrDefault(p => p.Name.Equals("Telnet")).Path = @"%windir%\sysnative\telnet.exe";
        }

        [JsonConverter(typeof(GenericListConverter<Program, IProgram>))]
        public IList<IProgram> Programs { get; private set; }

        [JsonConverter(typeof(GenericListConverter<Protocol, IProtocol>))]
        public IList<IProtocol> Protocols { get; private set; }

        [JsonConverter(typeof(GenericConverter<ServerGroup>))]
        public IServerGroup RootGroup { get; set; }

        public void Load(string path)
        {
            try
            {
                string input = File.ReadAllText(path);
                Config conf = JsonConvert.DeserializeObject<Config>(input);
                Programs = conf.Programs;
                Protocols = conf.Protocols;
                RootGroup = conf.RootGroup;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public void Save(string path)
        {
            try
            {
                string output = JsonConvert.SerializeObject(this);
                File.WriteAllText(path, output, Encoding.UTF8);
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
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
        public Config()
        {
            Programs = new List<IProgram>();
        }

        [JsonConverter(typeof(GenericListConverter<Program, IProgram>))]
        public IList<IProgram> Programs { get; protected set; }


        [JsonConverter(typeof(GenericConverter<ServerGroup>))]
        public IServerGroup RootGroup { get; set; }

        public void Load(string path)
        {
            try
            {
                string input = File.ReadAllText(path);
                Config conf = JsonConvert.DeserializeObject<Config>(input);
                Programs = conf.Programs;
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
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
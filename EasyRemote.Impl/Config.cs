using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using EasyRemote.Impl.Converters.JSON;
using EasyRemote.Spec;
using Newtonsoft.Json;

namespace EasyRemote.Impl
{
    internal class Config : IConfig
    {
        public Config()
        {
            Programs = new List<IProgram>();
        }

        [JsonConverter(typeof(ProgramConverter))]
        public IList<IProgram> Programs { get; protected set; }


        [JsonConverter(typeof(GenericConverter<ServerGroup>))]
        public IServerGroup RootGroup { get; set; }

        public void Load(string path)
        {
            try
            {
                var input = File.ReadAllText(path);
                var conf = JsonConvert.DeserializeObject<Config>(input);
                Programs = conf.Programs;
                RootGroup = conf.RootGroup;
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception e)
            {
                MessageBox.Show("Error while loading configurations from file...\r\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public void Save(string path)
        {
            try
            {
                var output = JsonConvert.SerializeObject(this);
                File.WriteAllText(path, output, Encoding.UTF8);
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception e)
            {
                MessageBox.Show("Error while saving configurations into file... \r\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
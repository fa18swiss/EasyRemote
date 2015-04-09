using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using EasyRemote.Impl.Converters.JSON;
using EasyRemote.Impl.JSON;
using EasyRemote.Spec;
using Microsoft.Practices.ObjectBuilder2;
using Newtonsoft.Json;

namespace EasyRemote.Impl
{
    public class ServerGroup : IServerGroup
    {
        public ServerGroup()
        {
            Childrens = new ObservableCollection<IServerBase>();
        }
        public ServerGroup(string name, params IServerBase[] servers):this()
        {
            Name = name;
            Childrens = new ObservableCollection<IServerBase>();
            servers.ForEach(s => Childrens.Add(s));
        }

        public ServerGroup(ServerBaseHelper helper) : this()
        {
            Name = helper.Name;
            Childrens = helper.Childrens;
        }

        [Browsable(true)]
        [Category("Settings")]
        public string Name { get; set; }

        [Browsable(false)]
        public ServerClassType Type
        {
            get
            {
                return ServerClassType.Group;
            }
        }

        [Browsable(false)] // not shown in property grid
        [JsonConverter(typeof(ServerBaseConverter))]
        public IList<IServerBase> Childrens { get; set; }

    }
}
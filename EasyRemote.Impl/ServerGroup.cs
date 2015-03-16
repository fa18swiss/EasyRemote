using System.Collections.Generic;
using System.Collections.ObjectModel;
using EasyRemote.Spec;
using Microsoft.Practices.ObjectBuilder2;

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
            servers.ForEach(s => Childrens.Add(s));
        }

        public string Name { get; set; }
        public IList<IServerBase> Childrens { get; private set; }
    }
}
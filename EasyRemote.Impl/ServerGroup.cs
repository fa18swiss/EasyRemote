using System.Collections.Generic;
using System.Collections.ObjectModel;
using EasyRemote.Spec;

namespace EasyRemote.Impl
{
    public class ServerGroup : IServerGroup
    {
        public ServerGroup()
        {
            Childrens = new ObservableCollection<IServerBase>();
        }

        public string Name { get; set; }
        public IList<IServerBase> Childrens { get; private set; }
    }
}
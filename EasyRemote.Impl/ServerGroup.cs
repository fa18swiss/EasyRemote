using System.Collections.Generic;
using EasyRemote.Spec;

namespace EasyRemote.Impl
{
    internal class ServerGroup : IServerGroup
    {
        public ServerGroup()
        {
            Childrens = new List<IServerBase>();
        }
        public string Name { get; set; }
        public IList<IServerBase> Childrens { get; private set; }
    }
}

using System.Collections.Generic;

namespace EasyRemote.Spec
{
    public interface IServerGroup : IServerBase
    {
        IList<IServerBase> Childrens { get; }
    }
}
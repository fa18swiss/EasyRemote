using System.Collections.Generic;

namespace EasyRemote.Spec
{
    /// <summary>
    /// Group of server
    /// </summary>
    public interface IServerGroup : IServerBase
    {
        /// <summary>
        /// Childrens
        /// </summary>
        IList<IServerBase> Childrens { get; }
    }
}
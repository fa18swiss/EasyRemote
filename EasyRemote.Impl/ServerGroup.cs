using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using EasyRemote.Impl.Converters.JSON;
using EasyRemote.Spec;
using Microsoft.Practices.ObjectBuilder2;
using Newtonsoft.Json;

namespace EasyRemote.Impl
{

    /// <summary>
    /// implementation for a group of servers
    /// </summary>
    public class ServerGroup : IServerGroup
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ServerGroup()
        {
            Childrens = new ObservableCollection<IServerBase>();
        }

        /// <summary>
        /// Copy constructor (used by json deserializazion)
        /// </summary>
        /// <param name="helper"></param>
        public ServerGroup(IServerGroup helper) : this()
        {
            Name = helper.Name;
            Childrens = helper.Childrens;
        }

        /* those annotation are used for the property grid
        // Browsable : determine if property must be showed in grid
        // Category : the grid group property into categories
        // DisplayName : the name that is displayed. Grid use property hard coded name by default
        */ 


        /// <summary>
        /// Group name
        /// </summary>
        [Browsable(true)]
        [Category("Settings")]
        public string Name { get; set; }

        /// <summary>
        /// Type used for JSON deserialization
        /// </summary>
        [Browsable(false)]
        public ServerClassType Type
        {
            get
            {
                return ServerClassType.Group;
            }
        }

        /// <summary>
        /// Childrens (list of IServerBase)
        /// </summary>
        [Browsable(false)] // not shown in property grid
        [JsonConverter(typeof(ServerBaseConverter))]
        public IList<IServerBase> Childrens { get; set; }

    }
}
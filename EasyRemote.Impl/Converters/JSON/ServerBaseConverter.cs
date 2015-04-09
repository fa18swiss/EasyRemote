using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyRemote.Spec;
using Newtonsoft.Json;

namespace EasyRemote.Impl.Converters.JSON
{
    internal class ServerBaseConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var tempList = serializer.Deserialize<IList<ServerBaseHelper>>(reader).ToList<ServerBaseHelper>();
            var returnList = new ObservableCollection<IServerBase>();
            foreach (var s in tempList)
            {
                Debug.Print(s.Type+ "");
                if (s.Type == ServerClassType.Server)
                {
                    Debug.Print("ASDASDASDAS");
                    returnList.Add(new Server(s));
                }
                else
                {
                    Debug.Print("OIDAIUSD");
                    returnList.Add(new ServerGroup(s));
                }
            }
            return returnList;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}

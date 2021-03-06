﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EasyRemote.Impl.JSON;
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
                if (s.Type == ServerClassType.Server)
                {
                    returnList.Add(new Server(s));
                }
                else
                {
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

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace EasyRemote.Impl.Converters.JSON
{
    internal class GenericListConverter<TClass, TInterface> : JsonConverter
        where TClass : class, TInterface  
        where TInterface : class
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            return serializer.Deserialize<IList<TClass>>(reader).ToList<TInterface>();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}

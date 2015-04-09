using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            return new ObservableCollection<TInterface>(serializer.Deserialize<IList<TClass>>(reader));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}

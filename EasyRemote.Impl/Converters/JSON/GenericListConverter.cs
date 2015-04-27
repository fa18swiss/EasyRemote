using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;


namespace EasyRemote.Impl.Converters.JSON
{
    /// <summary>
    /// Generic converter that read or write JSON and works with generic type of object T
    /// this converter Read and write lists of objects that implement same interface
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <typeparam name="TInterface"></typeparam>
    internal class GenericListConverter<TClass, TInterface> : JsonConverter
        where TClass : class, TInterface  
        where TInterface : class
    {
        /// <summary>
        /// return always true (override)
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        /// <summary>
        /// Deserialize JSON from reader and return a list of TInferface objects
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            return new ObservableCollection<TInterface>(serializer.Deserialize<IList<TClass>>(reader));
        }

        /// <summary>
        /// Serialize an object into JSON
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}

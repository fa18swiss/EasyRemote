using System;
using System.Linq;
using EasyRemote.Spec;
using Newtonsoft.Json;

namespace EasyRemote.Impl.Converters.JSON
{

    class ProtocolConverter : JsonConverter
    {
        /// <summary>
        /// list of programs and protocols that will be deserialized
        /// </summary>
        public static IProgramsProtocolsList ProgramsProtocolsList { get; set; }

        /// <summary>
        /// Always return true (override)
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        /// <summary>
        /// Deserialize the names (string) of protocols and return a protocol object
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var name = serializer.Deserialize<string>(reader);
            return ProgramsProtocolsList.Protocols.FirstOrDefault(p => p.Name == name);
        }

        /// <summary>
        /// Serialize a protocol in JSON using only its name (string)
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var protocol = value as IProtocol;
            if (protocol != null)
            {
                serializer.Serialize(writer, protocol.Name);
            }
        }
    }
}

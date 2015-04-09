using System;
using System.Linq;
using EasyRemote.Spec;
using Newtonsoft.Json;

namespace EasyRemote.Impl.Converters.JSON
{
    class ProtocolConverter : JsonConverter
    {
        public static IProgramsProtocolsList ProgramsProtocolsList { get; set; }
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var name = serializer.Deserialize<string>(reader);
            return ProgramsProtocolsList.Protocols.FirstOrDefault(p => p.Name == name);
        }

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

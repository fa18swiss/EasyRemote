using System;
using System.Collections.Generic;
using System.Linq;
using EasyRemote.Impl.JSON;
using EasyRemote.Spec;
using Newtonsoft.Json;

namespace EasyRemote.Impl.Converters.JSON
{
    class ProgramConverter : JsonConverter
    {
        public static IProgramsProtocolsList ProgramsProtocolsList { get; set; }
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var programs = serializer.Deserialize<List<ProgramJson>>(reader);
            programs.ForEach(p => p.ToProgram(ProgramsProtocolsList));
            return ProgramsProtocolsList.Programs;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var programs = value as IList<IProgram>;
            if (programs != null)
            {
                serializer.Serialize(writer, programs.Select(p => new ProgramJson(p)));
            }
        }
    }
}

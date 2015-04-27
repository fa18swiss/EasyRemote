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
        /// <summary>
        /// list of programs and protocols that will be deserialized
        /// </summary>
        public static IProgramsProtocolsList ProgramsProtocolsList { get; set; }

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
        /// Deserialize a list of program in JSON to program objects list
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var programs = serializer.Deserialize<List<ProgramJson>>(reader); //get a list of ProgramJson from json reader
            programs.ForEach(p => p.ToProgram(ProgramsProtocolsList)); //fill the programs and protocols list
            return ProgramsProtocolsList.Programs; //return only programs
        }

        /// <summary>
        /// Serialize a list of IProgram in JSON
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
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

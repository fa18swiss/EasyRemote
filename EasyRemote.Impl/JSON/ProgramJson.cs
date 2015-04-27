﻿using System.Linq;
using EasyRemote.Spec;

namespace EasyRemote.Impl.JSON
{
    /// <summary>
    /// Calss for json serialisation of programs
    /// </summary>
    internal class ProgramJson
    {
        /// <summary>
        /// Create program json from program
        /// </summary>
        /// <param name="program">source</param>
        public ProgramJson(IProgram program)
        {
            Name = program.Name;
            Path = program.Path;
            IsActivate = program.IsActivate;
        }
        /// <summary>
        /// Map program from list
        /// </summary>
        /// <param name="programsProtocolsList">List of program</param>
        /// <returns>porgram</returns>
        public IProgram ToProgram(IProgramsProtocolsList programsProtocolsList)
        {
            var program = programsProtocolsList.Programs.FirstOrDefault(p => p.Name == Name);
            if (program != null)
            {
                program.Path = Path;
                program.IsActivate = IsActivate;
            }
            return program;
        }

        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsActivate { get; set; }
    }
}

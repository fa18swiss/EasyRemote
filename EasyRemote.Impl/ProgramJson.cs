using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EasyRemote.Spec;

namespace EasyRemote.Impl
{
    internal class ProgramJson
    {
        public ProgramJson()
        {
            Name = Path = null;
        }

        public ProgramJson(IProgram program)
        {
            Name = program.Name;
            Path = program.Path;
        }

        public IProgram ToProgram(IProgramsProtocolsList programsProtocolsList)
        {
            var program = programsProtocolsList.Programs.FirstOrDefault(p => p.Name == Name);
            if (program != null)
            {
                program.Path = Path;
            }
            return program;
        }

        public string Name { get; set; }
        public string Path { get; set; }
    }
}

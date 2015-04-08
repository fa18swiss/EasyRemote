using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyRemote.ProgramsProtocols.Util
{
    public static class AssemblyUtil
    {
        public static IEnumerable<Type> GetTypesInNamespace(this Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal));
        }
    }
}

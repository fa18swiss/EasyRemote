using System;
using System.Diagnostics;
using EasyRemote.Spec;

namespace EasyRemote.Impl.Extension
{
    public static class Path
    {
        public static string GetPath(this IProgram protocol)
        {
            Debug.Print("{0} => {1}", protocol.Path, Environment.ExpandEnvironmentVariables(protocol.Path));
            return Environment.ExpandEnvironmentVariables(protocol.Path);
        }
    }
}
using System;
using System.Diagnostics;
using EasyRemote.Spec;

namespace EasyRemote.Impl.Extension
{
    public static class Path
    {
        /// <summary>
        /// Get path for program
        /// </summary>
        /// <param name="program">Program</param>
        /// <returns>Full path</returns>
        public static string GetPath(this IProgram program)
        {
            Debug.Print("{0} => {1}", program.Path, Environment.ExpandEnvironmentVariables(program.Path));
            return Environment.ExpandEnvironmentVariables(program.Path);
        }
    }
}
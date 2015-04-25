using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace EasyRemote.ProgramsProtocols.Util
{
    public static class AssemblyUtil
    {
        /// <summary>
        /// List all type in namespace
        /// </summary>
        /// <param name="assembly">Assemply source</param>
        /// <param name="nameSpace">namespace</param>
        /// <returns>List of type</returns>
        public static IEnumerable<Type> GetTypesInNamespace(this Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal));
        }
        /// <summary>
        /// Register all type in namesapce
        /// </summary>
        /// <typeparam name="TInterface">Interface register</typeparam>
        /// <typeparam name="TLifetimeManager">Lifetime manager</typeparam>
        /// <param name="container">container</param>
        /// <param name="namespaceName">namesapceSource</param>
        public static void RegisterAllclassFromNamespace<TInterface, TLifetimeManager>(this IUnityContainer container,
            string namespaceName)
            where TLifetimeManager : LifetimeManager, new()
        {
            var assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypesInNamespace(namespaceName).Where(t => !t.IsAbstract))
            {
                container.RegisterType(typeof (TInterface), type, type.Name, new TLifetimeManager());
            }
        }
    }
}
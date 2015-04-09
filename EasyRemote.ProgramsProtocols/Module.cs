using System.Linq;
using System.Reflection;
using EasyRemote.ProgramsProtocols.Util;
using EasyRemote.Spec;
using EasyRemote.Spec.Module;
using Microsoft.Practices.Unity;

namespace EasyRemote.ProgramsProtocols
{
    public class Module : IModule
    {
        public void Load(IUnityContainer container)
        {
            var assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypesInNamespace(GetType().Namespace + ".Protocols").Where(t => !t.IsAbstract))
            {
                container.RegisterType(typeof (IProtocol), type, type.Name, new ContainerControlledLifetimeManager());
            }
            foreach (var type in assembly.GetTypesInNamespace(GetType().Namespace + ".Programs").Where(t => !t.IsAbstract))
            {
                container.RegisterType(typeof(IProgram), type, type.Name, new ContainerControlledLifetimeManager());
            }
        }
    }
}

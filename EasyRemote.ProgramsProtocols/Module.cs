using EasyRemote.ProgramsProtocols.Util;
using EasyRemote.Spec;
using EasyRemote.Spec.Module;
using Microsoft.Practices.Unity;

namespace EasyRemote.ProgramsProtocols
{
    /// <summary>
    /// Module
    /// </summary>
    public class Module : IModule
    {
        public void Load(IUnityContainer container)
        {
            container.RegisterAllclassFromNamespace<IProtocol, ContainerControlledLifetimeManager>(GetType().Namespace +
                                                                                                   ".Protocols");
            container.RegisterAllclassFromNamespace<IProgram, ContainerControlledLifetimeManager>(GetType().Namespace +
                                                                                                  ".Programs");
        }
    }
}
using EasyRemote.ProgramsProtocols.Programs;
using EasyRemote.ProgramsProtocols.Protocols;
using EasyRemote.Spec;
using EasyRemote.Spec.Module;
using Microsoft.Practices.Unity;

namespace EasyRemote.ProgramsProtocols
{
    public class Module : IModule
    {
        public void Load(IUnityContainer container)
        {
            container.RegisterType<IProtocol, FTP>("FTP", new ContainerControlledLifetimeManager());
            container.RegisterType<IProtocol, FTPS>("FTPS", new ContainerControlledLifetimeManager());
            container.RegisterType<IProtocol, HTTP>("HTTP", new ContainerControlledLifetimeManager());
            container.RegisterType<IProtocol, HTTPS>("HTTPS", new ContainerControlledLifetimeManager());
            container.RegisterType<IProtocol, SSH>("SSH", new ContainerControlledLifetimeManager());
            container.RegisterType<IProtocol, Telnet>("Telnet", new ContainerControlledLifetimeManager());
            container.RegisterType<IProtocol, VNC>("VNC", new ContainerControlledLifetimeManager());

            container.RegisterType<IProgram, FileZilla>("FileZilla", new ContainerControlledLifetimeManager());
            container.RegisterType<IProgram, Putty>("Putty", new ContainerControlledLifetimeManager());
            container.RegisterType<IProgram, TurboVNC>("TurboVNC", new ContainerControlledLifetimeManager());
            container.RegisterType<IProgram, WinSCP>("WinSCP", new ContainerControlledLifetimeManager());
        }
    }
}

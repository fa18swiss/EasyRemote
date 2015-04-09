using EasyRemote.Impl.Facory;
using EasyRemote.Spec;
using EasyRemote.Spec.Factory;
using EasyRemote.Spec.Module;
using Microsoft.Practices.Unity;

namespace EasyRemote.Impl.Module
{
    public class ImplModule : IModule
    {
        public void Load(IUnityContainer container)
        {
            // factorys
            container.RegisterType<IFactory<IServer>, GenericFactory<IServer, Server>>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<IFactory<IServerGroup>, GenericFactory<IServerGroup, ServerGroup>>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<IFactory<IServerProtocol>, GenericFactory<IServerProtocol, ServerProtocol>>(
                new ContainerControlledLifetimeManager());

            // config
            container.RegisterType<IProgramsProtocolsList, ProgramsProtocolsList>(new ContainerControlledLifetimeManager());
            container.RegisterType<IConfig, DefaultConfig>(new ContainerControlledLifetimeManager());

            Converters.JSON.ProtocolConverter.ProgramsProtocolsList = container.Resolve<IProgramsProtocolsList>();
        }
    }
}
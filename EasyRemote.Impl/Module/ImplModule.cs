using EasyRemote.Impl.Facory;
using EasyRemote.Impl.Settings;
using EasyRemote.Spec;
using EasyRemote.Spec.Factory;
using EasyRemote.Spec.Module;
using EasyRemote.Spec.Settings;
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
            container.RegisterType<IProgramsProtocolsList, ProgramsProtocolsList>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<IConfig, DefaultConfig>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUserSettings, UserSettings>();

            var programsProtocolsList = container.Resolve<IProgramsProtocolsList>();
            Converters.JSON.ProtocolConverter.ProgramsProtocolsList = programsProtocolsList;
            Converters.JSON.ProgramConverter.ProgramsProtocolsList = programsProtocolsList;
        }
    }
}
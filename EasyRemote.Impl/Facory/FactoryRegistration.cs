using EasyRemote.Spec.Factory;
using Microsoft.Practices.Unity;

namespace EasyRemote.Impl.Facory
{
    internal static class FactoryRegistration
    {
        /// <summary>
        /// Register factory
        /// </summary>
        /// <typeparam name="TInterface">Type of interface</typeparam>
        /// <typeparam name="TClass">Type of class</typeparam>
        /// <param name="container">container</param>
        /// <param name="lifetimeManager">lifetime manager</param>
        public static void RegisterFactory<TInterface, TClass>(this IUnityContainer container, LifetimeManager lifetimeManager)
            where TInterface : class 
            where TClass : TInterface , new()
        {
            container.RegisterType<IFactory<TInterface>, GenericFactory<TInterface, TClass>>(lifetimeManager);
        }
    }
}

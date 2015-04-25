using EasyRemote.Spec.Factory;

namespace EasyRemote.Impl.Facory
{
    /// <summary>
    /// Generic factory
    /// </summary>
    /// <typeparam name="TInterface">Type of interface registerd</typeparam>
    /// <typeparam name="TClass">Type of class instancied</typeparam>
    public class GenericFactory<TInterface, TClass> : IFactory<TInterface>
        where TInterface : class
        where TClass : TInterface, new()

    {
        public TInterface Create()
        {
            return new TClass();
        }
    }
}
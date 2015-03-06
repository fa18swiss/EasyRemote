using EasyRemote.Spec.Factory;

namespace EasyRemote.Impl.Facory
{
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
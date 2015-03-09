using Microsoft.Practices.Unity;

namespace EasyRemote.Spec.Module
{
    public interface IModule
    {
        void Load(IUnityContainer container);
    }
}
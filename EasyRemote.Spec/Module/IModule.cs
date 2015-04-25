using Microsoft.Practices.Unity;

namespace EasyRemote.Spec.Module
{
    /// <summary>
    /// Module for dependency injection
    /// </summary>
    public interface IModule
    {
        void Load(IUnityContainer container);
    }
}
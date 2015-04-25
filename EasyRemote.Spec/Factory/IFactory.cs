namespace EasyRemote.Spec.Factory
{
    /// <summary>
    /// Factory for create new instance of class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFactory<out T> where T : class
    {
        T Create();
    }
}
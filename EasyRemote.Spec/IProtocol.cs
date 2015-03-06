namespace EasyRemote.Spec
{
    public interface IProtocol
    {
        string Name { get; set; }
        int DefaultPort { get; set; }
    }
}
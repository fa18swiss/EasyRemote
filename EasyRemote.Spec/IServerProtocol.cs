namespace EasyRemote.Spec
{
    public interface IServerProtocol
    {
        int Port { get; set; }
        IProtocol Protocol { get; set; }
    }
}
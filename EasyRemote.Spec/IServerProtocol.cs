namespace EasyRemote.Spec
{
    public interface IServerProtocol
    {
        int? Port { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        IProtocol Protocol { get; set; }
    }
}
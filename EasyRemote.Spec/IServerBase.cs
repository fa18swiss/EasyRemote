using System.Security.Cryptography.X509Certificates;

namespace EasyRemote.Spec
{
    public interface IServerBase
    {
        string Name { get; set; }
        ServerClassType Type { get; }
    }

    public enum ServerClassType
    {
        Server, Group
    }
}
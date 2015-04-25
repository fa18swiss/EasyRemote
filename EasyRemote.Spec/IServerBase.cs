namespace EasyRemote.Spec
{
    /// <summary>
    /// Top class of design pattern coposition
    /// </summary>
    public interface IServerBase
    {
        /// <summary>
        /// Name
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Type (for serialisation)
        /// </summary>
        ServerClassType Type { get; }
    }

    public enum ServerClassType
    {
        Server = 0, Group = 1
    }
}
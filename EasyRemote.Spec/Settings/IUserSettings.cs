namespace EasyRemote.Spec.Settings
{
    /// <summary>
    /// Application settings for user
    /// </summary>
    public interface IUserSettings
    {
        /// <summary>
        /// Last path used for config
        /// </summary>
        string LastPath { get; set; }
    }
}

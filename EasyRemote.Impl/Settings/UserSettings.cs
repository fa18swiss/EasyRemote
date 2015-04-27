using EasyRemote.Spec.Settings;

namespace EasyRemote.Impl.Settings
{
    /// <summary>
    /// User's parameters implementation
    /// </summary>
    internal class UserSettings : IUserSettings
    {
        /// <summary>
        /// last path used for opening json serialized configs
        /// </summary>
        private string lastPath;

        /// <summary>
        /// property with some tests before getting/setting last path
        /// </summary>
        public string LastPath
        {
            get
            {
                if (lastPath == null)
                {
                    lastPath = Settings.Default.LastPath;
                }
                if (string.IsNullOrEmpty(lastPath))
                {
                    lastPath = null;
                }
                return lastPath;
            }
            set
            {
                if (string.Equals(lastPath, value)) return;
                Settings.Default.LastPath = value;
                Settings.Default.Save();
                lastPath = value;
            }
        }
    }
}

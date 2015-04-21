using EasyRemote.Spec.Settings;

namespace EasyRemote.Impl.Settings
{
    internal class UserSettings : IUserSettings
    {
        private string lastPath;
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

using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace EasyRemote
{
    static class IconsTools
    {
        public static BitmapFrame ToBitmapFrame(this Icon icon)
        {
            if (icon != null)
            {
                using (var iconStream = new MemoryStream())
                {
                    icon.Save(iconStream);
                    iconStream.Seek(0, SeekOrigin.Begin);
                    return BitmapFrame.Create(iconStream);
                }
            }
            return null;
        }

        public static Icon GetProgramIcon(this string path)
        {
            return File.Exists(path) ? Icon.ExtractAssociatedIcon(path) : null;
        }
    }
}

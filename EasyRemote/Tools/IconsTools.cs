using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace EasyRemote.Tools
{
    static class IconsTools
    {
        /// <summary>
        /// Convert icon to bitmap frame
        /// </summary>
        /// <param name="icon">Icon</param>
        /// <returns>Bitmap frame</returns>
        public static BitmapFrame ToBitmapFrame(this Icon icon)
        {
            if (icon == null) return null;
            using (var iconStream = new MemoryStream())
            {
                icon.Save(iconStream);
                iconStream.Seek(0, SeekOrigin.Begin);
                return BitmapFrame.Create(iconStream);
            }
        }
        /// <summary>
        /// Return icon of executable
        /// </summary>
        /// <param name="path">Path of executable</param>
        /// <returns>Icon or null</returns>
        public static Icon GetProgramIcon(this string path)
        {
            return File.Exists(path) ? Icon.ExtractAssociatedIcon(path) : null;
        }
    }
}

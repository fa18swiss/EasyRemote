using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using EasyRemote.Impl.Extension;
using EasyRemote.Spec;

namespace EasyRemote.Convertes
{
    public class ProgramImageConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var program = value as IProgram;
            if (program != null)
            {
                var bitmap = program.GetPath().GetProgramIcon().ToBitmapFrame();
                if (bitmap != null)
                {
                    return bitmap;
                }
            }
            return new BitmapImage(new Uri("Images/program.png", UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}

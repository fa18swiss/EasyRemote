using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using EasyRemote.Impl.Extension;
using EasyRemote.Spec;

namespace EasyRemote.Converters
{
    /// <summary>
    /// Converter that return icon of program, of default icone
    /// </summary>
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

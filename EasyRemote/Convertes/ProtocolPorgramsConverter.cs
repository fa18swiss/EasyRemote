using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using EasyRemote.Spec;

namespace EasyRemote.Convertes
{
    public class ProtocolPorgramsConverter : IValueConverter
    {
        // TODO change, this is bad !
        public static IConfig Config { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var serverProtocol = value as IServerProtocol;
            if (serverProtocol != null)
            {
                return Config.Programs.Where(p => p.Protocols.Contains(serverProtocol.Protocol));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}

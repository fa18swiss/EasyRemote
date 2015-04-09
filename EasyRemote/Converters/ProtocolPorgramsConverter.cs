using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using EasyRemote.Spec;

namespace EasyRemote.Converters
{
    public class ProtocolPorgramsConverter : IValueConverter
    {
        // TODO change, this is bad !
        public static IProgramsProtocolsList ProgramsProtocolsList { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var serverProtocol = value as IServerProtocol;
            if (serverProtocol != null)
            {
                return ProgramsProtocolsList.Programs.Where(p => p.Protocols.Contains(serverProtocol.Protocol));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}

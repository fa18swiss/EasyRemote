using System;
using System.Globalization;
using System.Windows.Data;
using EasyRemote.Spec;

namespace EasyRemote.Convertes
{
    public class ProtocolPortConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var serverProtocol = value as IServerProtocol;
            if (serverProtocol != null)
            {
                return serverProtocol.Port.HasValue ? serverProtocol.Port.Value.ToString() : string.Format("{0} (default)",serverProtocol.Protocol.DefaultPort);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}

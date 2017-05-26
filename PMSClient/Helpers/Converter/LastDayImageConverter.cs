using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PMSClient.Helpers.Converter
{
    public class LastDayImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime deliveryDate = (DateTime)value;
            TimeSpan days = deliveryDate - DateTime.Now;
            if (days.Days <= 0)
            {
                return new BitmapImage(new Uri(@"..\Resource\Icons\flag.png", UriKind.Relative));
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

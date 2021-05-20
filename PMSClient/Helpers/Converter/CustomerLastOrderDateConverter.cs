using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace PMSClient.Helpers.Converter
{
    public class CustomerLastOrderDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            if(date.Year==DateTime.Now.Year)
            {
                System.Diagnostics.Debug.Print("color.blue");
                return new SolidColorBrush(Colors.Blue);
            }
            {
                return new SolidColorBrush(Colors.Black);
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

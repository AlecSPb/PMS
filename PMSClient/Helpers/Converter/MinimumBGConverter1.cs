using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace PMSClient.Helpers.Converter
{
    public class MinimumBGConverter1 : IValueConverter
    {
        public double MinimumValue { get; set; } = 0.9;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double threshold = (double)value;
            if (threshold < MinimumValue && threshold > 0)
            {
                return new SolidColorBrush(Colors.LightSalmon);
            }
            else
            {
                return new SolidColorBrush(Colors.Transparent);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PMSClient.Helpers.Converter
{
    public class StringCutConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "None";
            }
            string s = value.ToString();
            if (s == "")
            {
                return "Empty";
            }
            if (s.Length > 20)
            {
                return s.Substring(0, 20);
            }
            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

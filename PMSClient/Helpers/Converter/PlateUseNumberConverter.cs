using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PMSClient.MainService;
using System.Text.RegularExpressions;

namespace PMSClient.Helpers.Converter
{
    class PlateUseNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = value.ToString();
            if (string.IsNullOrEmpty(s)) return "";

            return GetUsedTimes(s);
        }

        private int GetUsedTimes(string s)
        {
            return Regex.Matches(s, @"A", RegexOptions.IgnoreCase).Count;
        } 


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

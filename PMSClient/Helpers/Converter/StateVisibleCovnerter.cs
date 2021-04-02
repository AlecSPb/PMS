using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PMSClient.Helpers.Converter
{
    public class StateVisibleCovnerter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string state = value.ToString();
            if (!string.IsNullOrEmpty(state))
            {
                if (state == PMSCommon.MaterialOrderItemState.未完成.ToString())
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Hidden;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }


}

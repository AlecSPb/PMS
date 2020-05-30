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
    public class IsOverDueBGConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int aheaddays = 0;

            DateTime deadline = (DateTime)values[0];
            string state = values[1].ToString();
            TimeSpan ts = deadline - DateTime.Today;
            if (state == PMSCommon.OrderState.未完成.ToString())
            {
                int leftDays = (int)ts.TotalDays;
                if (leftDays <= aheaddays)
                {
                    return new SolidColorBrush(Colors.Yellow);
                }
                else
                {
                    return new SolidColorBrush(Colors.White);
                }
            }

            return new SolidColorBrush(Colors.White);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

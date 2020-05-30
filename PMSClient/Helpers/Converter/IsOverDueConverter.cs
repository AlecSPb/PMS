using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PMSClient.Helpers.Converter
{
    public class IsOverDueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            DateTime deadline = (DateTime)values[0];
            string state = values[1].ToString();
            TimeSpan ts = deadline - DateTime.Today;
            if (state == PMSCommon.OrderState.未完成.ToString())
            {
                int leftDays = (int)ts.TotalDays;
                return leftDays.ToString();
            }

            return "";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PMSClient.BarCodeService;

namespace PMSClient.Helpers.Converter
{
    public class IDToBarCodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string productid = value.ToString();
            if (productid != null)
            {
                BarCodeHelper helper = new BarCodeHelper();
                return helper.CreateBarCodeBmp(productid);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

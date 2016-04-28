using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFValidationRule
{
    public class NumberRangeRule : ValidationRule
    {
        public double Min { get; set; }
        public double Max { get; set; }
        public string ErrorMesage { get; set; }

        public NumberRangeRule()
        {
            ErrorMesage = $"the value must be between {Min}-{Max}";

            Min = double.MinValue;
            Max = double.MaxValue;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double input = 0;
            double.TryParse(value.ToString(), out input);
            if (input > Min && input < Max)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, ErrorMesage);
            }
        }
    }
}

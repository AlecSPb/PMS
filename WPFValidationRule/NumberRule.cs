using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFValidationRule
{
    public class NumberRule : ValidationRule
    {
        public string ErrorMesage { get; set; }
        public NumberRule()
        {
            ErrorMesage = "Must be a number!";
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value.ToString();
            //利用正则表达式的方法来验证是否是浮点数字
            Regex regex = new Regex(@"^[-]?\d+[.]?\d*$");
            if (regex.IsMatch(input))
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

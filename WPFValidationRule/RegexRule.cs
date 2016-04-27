using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

/*
    Developer:xs.zhou@outlook.com
    CreateTime:2016/4/25 15:59:31
*/
namespace WPFValidationRule
{
    public class RegexRule:ValidationRule
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        public string PatternString { get; set; }

        public string ErrorMesage { get; set; }

        public RegexRule()
        {
            ErrorMesage = "Your input did not pass the regex match.";
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value.ToString();
            if (Regex.IsMatch(input,PatternString))
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

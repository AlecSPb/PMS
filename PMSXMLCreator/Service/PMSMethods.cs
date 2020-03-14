using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PMSXMLCreator.Service
{
    public static class PMSMethods
    {
        public static void SetTextBox(TextBox control, string text)
        {
            if (control != null)
            {
                control.Focus();
                control.Text = text;
            }
        }
        public static void SetTextBoxAppend(TextBox control, string text)
        {
            if (control != null)
            {
                control.Focus();
                control.Text += text;

            }
        }

        public static void RemoveSomeCharacters(TextBox control)
        {
            if (control != null)
            {
                control.Focus();
                control.Text = control.Text.Replace("无", "");
            }
        }

        /// <summary>
        /// 传入的T必须是Enum
        /// </summary>
        /// <typeparam name="T">必须是枚举类型</typeparam>
        /// <param name="ds">必须提前new</param>
        public static void SetListDS<T>(List<string> ds)
        {
            if (ds != null)
            {
                ds.Clear();
                Enum.GetNames(typeof(T)).ToList().ForEach(i => ds.Add(i));
            }
        }

        /// <summary>
        /// 传入的T必须是Enum
        /// </summary>
        private static List<string> GetEnumNames<T>()
        {
            return Enum.GetNames(typeof(T)).ToList();
        }
    }
}

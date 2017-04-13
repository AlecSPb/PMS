using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PMSClient
{
    public static class PMSMethods
    {
        public static void SetTextBox(TextBox control,string text)
        {
            if (control!=null)
            {
                control.Text = text;
                control.Focus();
            }
        }
    }
}

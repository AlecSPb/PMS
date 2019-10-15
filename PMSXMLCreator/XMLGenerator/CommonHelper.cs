using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PMSXMLCreator
{
    public static class CommonHelper
    {
        public static void ShowMessage(string msg)
        {
            MessageBox.Show(msg,"提示",MessageBoxButton.OK,MessageBoxImage.Information);
        }
    }
}

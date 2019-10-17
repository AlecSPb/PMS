using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PMSXMLCreator.XMLGenerator
{
    public static class Helper
    {

        public static string GDMS { get; set; } =
                 "Al=0 Bi=0 Cr=0 Cu=0 Fe=0 Ga=0 Mg=0 Mn=0 Ni=0 Pb=0 Sc=0 S=0 Ti=0 W=0 Y=0 Total=0";
        public static string VPI { get; set; } =
         "Oxygen=0 Nitrogen=0";


        public static void ShowMessage(string msg)
        {
            MessageBox.Show(msg, "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public static bool ShowDialog(string msg)
        {
            return
                MessageBox.Show(msg, "提示", MessageBoxButton.YesNo, MessageBoxImage.Question)
                == MessageBoxResult.Yes;
        }
    }
}

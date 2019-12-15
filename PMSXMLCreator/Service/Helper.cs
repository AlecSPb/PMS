using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PMSXMLCreator.Service
{
    public static class Helper
    {
        public static string Plate { get; set; } =
            "BP Diameter=0;BP Flange Thickness=0;BP Sidewall Height=0;BP Pocket Depth=0;BP Side Wall Dia=0;" +
            "BP Pocket Dia=0;BP Bonded Surface Flatness=0;" +
            "Target Blank 2mm Top Rad=Go;" +
            "Target Blank Flatness=0;Target Blank Parallism=0;Target Blank Surface Roughness=0;" +
            "BP Backside Flatness=0;BP Overall Height=0;";

        public static string XRF { get; set; } = "Se=51.0;As=30.6;Ge=12.7;Si=5.7;";
        public static string GDMS { get; set; } =
            "Al=0;Bi=0;Cr=0;Cu=0;Fe=0;Ga=0;Mg=0;Mn=0;Ni=0;Pb=0;Sc=0;S=0;Ti=0;W=0;Y=0;Total Metallic Impurities=0;";
        public static string VPI { get; set; } =
         "O=0;N=0;";


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

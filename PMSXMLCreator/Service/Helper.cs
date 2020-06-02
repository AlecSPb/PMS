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
        public static string Plate { get; set; } = "BP Diameter=575.01;BP Flange Thickness=16.05;BP Sidewall Height=82.53;BP Pocket Depth=76.46;BP Side Wall Dia=485.9;BP Pocket Dia=458.04;BP Target Blank Step=1;BP Bonded Surface Flatness=0.03;Target Blank 2mm Top Rad=Go;Target Blank Flatness=0.074;Target Blank Parallism=0.053;Target Blank Surface Roughness=0.75;";

        public static string XRF { get; set; } = "Se=51.0;As=30.6;Ge=12.7;Si=5.7;";
        public static string GDMS { get; set; } =
            "Al=0.77;Bi=0.13;Cr=0.54;Cu=0.72;Fe=1.2;Ga=0.45;Mg=0.15;Mn=0.12;Ni=0.67;Pb=0.005;Sc=0.48;S=2.1;Ti=1.30;W=0.005;Y=0.08;Total Metallic Impurities=8.72;";
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonHelper;

namespace CommonHelperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            XSDialogResult result = XSHelper.DialogHelper.ShowFontDialog();
            Console.WriteLine(result.HasSelected);
            Console.WriteLine(result.SelectedFont?.FontFamily);
            Console.WriteLine(result.SelectedFont?.Size);

            Console.Read();
        }
    }
}

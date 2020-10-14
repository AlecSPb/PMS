using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSXMLCreator_Micron.Model;
using System.IO;

namespace PMSXMLCreator_Micron.Service
{
    public static class FileSaver
    {

        public static void SaveText(string inputText,string filename)
        {
            File.WriteAllText(filename, inputText);
        }
        public static string LoadText(string filename)
        {
            if (File.Exists(filename))
            {
                return File.ReadAllText(filename);
            }
            return File.ReadAllText("template_default.txt");
        }


    }
}

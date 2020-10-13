using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSXMLCreator_Micron.Service
{
    public class Logger
    {
        private string file_name = "log.txt";
        public void LogIt(string info)
        {
            StreamWriter sw = File.CreateText(file_name);
            sw.WriteLine(info);
            sw.Close();
        }
    }
}

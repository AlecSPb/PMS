using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PMSXMLCreator.Service
{
    public class Log
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

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
        public void LogIt(string info,string logfilename= "OutputFile\\log.txt")
        {
            StreamWriter sw = File.CreateText(logfilename);
            sw.WriteLine(info);
            sw.Close();

            Properties.Settings.Default.LastLogFileName = logfilename;
            Properties.Settings.Default.Save();
        }
    }
}

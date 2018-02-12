using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataOutputTempProgram
{
    public static class Helper
    {
        public static string Process(string input)
        {
            return input.Substring(0, 8).Replace("M", "A").Replace("N", "B").Replace("O", "C");
        }
    }
}

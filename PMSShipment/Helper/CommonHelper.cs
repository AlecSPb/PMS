using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSShipment
{
    public class CommonHelper
    {
        public static string AppendHistory(string currenthistory, string state)
        {
            string s;

            if (currenthistory.Contains(state))
            {
                s = currenthistory;
            }
            else
            {
                if (string.IsNullOrEmpty(currenthistory))
                {
                    s = $"{DateTime.Today.ToString("yyMMdd")}-{state};";
                }
                else
                {
                    s = $"{DateTime.Today.ToString("yyMMdd")}-{state};{currenthistory}";
                }
            }

            return s;
        }
    }
}

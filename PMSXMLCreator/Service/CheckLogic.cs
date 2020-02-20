using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSXMLCreator.Service
{
    public static class CheckLogic
    {
        public static bool CheckLotNumber(string lotnumber)
        {
            return lotnumber.Contains("#");
        }

 
    }
}

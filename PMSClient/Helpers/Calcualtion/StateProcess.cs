using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSCommon
{
    public class StateProcess
    {
        /// <summary>
        /// StateToInt
        /// true=0,false=-1
        /// </summary>
        /// <param name="boolState"></param>
        /// <returns></returns>
        public static int BoolToInt(bool boolState)
        {
            return boolState ? 0 : -1;
        }
        /// <summary>
        /// StateToBool
        /// -1=false,other=true
        /// </summary>
        /// <param name="intState"></param>
        /// <returns></returns>
        public static bool InToBool(int intState)
        {
            if (intState == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

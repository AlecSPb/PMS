using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSHelper.Helpers
{
    /// <summary>
    /// 返回和本计算机xig
    /// </summary>
    public class ComputerInfoHelper
    {
        public string GetComputerName()
        {
            return Environment.MachineName;
        }

        public string GetCurrentUserName()
        {
            return Environment.UserName;
        }
    }
}

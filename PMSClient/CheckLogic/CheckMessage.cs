using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.CheckLogic
{
    public class CheckResult
    {
        public bool IsCheckOK { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}

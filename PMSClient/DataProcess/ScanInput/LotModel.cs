using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.DataProcess.ScanInput
{
    public class LotModel
    {
        public LotModel()
        {
            Lot = string.Empty;
            IsValid = true;
            HasProcessed = false;
            ExceptionMessage = string.Empty;
        }
        public bool IsValid { get; set; }

        public bool HasProcessed { get; set; }

        public string Lot { get; set; }

        public string ExceptionMessage { get; set; }

        public void AppendMessage(string msg)
        {
            ExceptionMessage += "+" + msg;
        }
    }
}

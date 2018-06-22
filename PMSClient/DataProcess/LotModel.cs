using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.DataProcess
{
    public class LotModel
    {
        public LotModel()
        {
            Lot = string.Empty;
            DataTableSource = TableSource.Unknown;
            IsValid = false;
            ExceptionMessage = string.Empty;
        }
        public bool IsValid { get; set; }
        public string Lot { get; set; }

        public TableSource DataTableSource { get; set; }
        public string ExceptionMessage { get; set; }
    }
}

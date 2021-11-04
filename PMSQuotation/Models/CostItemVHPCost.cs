using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSQuotation.Models
{
    public class CostItemVHPCost
    {
        public string Machine { get; set; }
        public double UnitPrice { get; set; }
        public double MachineTime { get; set; }//允许小数
    }
}

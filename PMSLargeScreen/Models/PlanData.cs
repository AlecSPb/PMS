using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSLargeScreen.Models
{
    public class PlanData
    {
        public Guid ID { get; set; }
        public string CompositionStd { get; set; }
        public string Customer { get; set; }
        public string DeviceCode { get; set; }
        public string ProcessCode { get; set; }
        public double SingleWeight { get; set; }
        public double TotalWeight { get; set; }
        public double MoldDiameter { get; set; }
        public double Thickness { get; set; }
        public int Quantity { get; set; }
        public string Remark { get; set; }

    }
}

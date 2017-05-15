using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Tool.Models
{
    public class MaterialNeedCalculationItem
    {
        public Guid ID { get; set; }
        public double Diameter { get; set; }
        public double Thickness { get; set; }
        public int Quantity { get; set; }
        public double WeightLoss { get; set; }
        public double Weight { get; set; }
        public string Remark { get; set; }
    }
}

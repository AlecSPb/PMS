using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 计划项
    /// </summary>
    public class PlanItem
    {
        public Guid ID { get; set; }
        public Guid PlanID { get; set; }
        public Guid OrderID { get; set; }
        public double CalculationDensity { get; set; }
        public double Thickness { get; set; }
        public int Quantity { get; set; }
        public string MillingRequirement { get; set; }

        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }

    }
}

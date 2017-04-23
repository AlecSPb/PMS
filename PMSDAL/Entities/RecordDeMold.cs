using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 取模记录
    /// </summary>
    public class RecordDeMold
    {

        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string State { get; set; }

        public string VHPPlanLot { get; set; }
        public string Composition { get; set; }
        public string PlanType { get; set; }
        public string BlankDimension { get; set; }
        public double CalculationDensity { get; set; }
        public double Density { get; set; }
        public double RatioDensity { get; set; }

        public string Temperature1 { get; set; }
        public string Temperature2 { get; set; }

        public string DeMoldType { get; set; }
        public double Weight { get; set; }
        public double Diameter1 { get; set; }
        public double Diameter2 { get; set; }

        public double Thickness1 { get; set; }
        public double Thickness2 { get; set; }
        public double Thickness3 { get; set; }
        public double Thickness4 { get; set; }
        public string Remark { get; set; }
    }
}

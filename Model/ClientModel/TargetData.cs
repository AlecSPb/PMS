using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 靶材
    /// 主要用于VHPProcessRecordSheet的生成
    /// </summary>
    public class TargetData
    {
        public Guid ID { get; set; }
        public Guid OrderID { get; set; }
        //public string StandardComposition { get; set; }
        //public string CustomerName { get; set; }
        //public string PO { get; set; }

        public double Thickness { get; set; }
        public double CalcualtionDensity { get; set; }
        public double Quantity { get; set; }

        public string MaterialSource { get; set; }
        public string GrainSize { get; set; }
        public string PowderWeight { get; set; }

        public string MoveOutTemperature { get; set; }
        public string TakeOutTemperature { get; set; }
        public string ExtraInformation { get; set; }


        public double RoughTargetWeight { get; set; }
        public double Diameter1 { get; set; }
        public double Diameter2 { get; set; }

        public double Thickness1 { get; set; }
        public double Thickness2 { get; set; }
        public double Thickness3 { get; set; }
        public double Thickness4 { get; set; }

        public bool NeedRecycled { get; set; }


    }
}

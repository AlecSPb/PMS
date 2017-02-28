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
    public class RecordTakeOut
    {

        public Guid ID { get; set; }
        public string Creator { get; set; }
        public string CreateTime { get; set; }
        public string State { get; set; }

        public Guid PlanID { get; set; }//Foreign Key
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

        public string WithExtraThickness { get; set; }//有额外的厚度

    }
}

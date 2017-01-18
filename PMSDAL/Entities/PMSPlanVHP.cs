using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{

    /// <summary>
    /// 热压计划
    /// 计划是按照热压日期，热压设备和热压模具来唯一确定的
    /// 这个热压计划只用来指导一次热压活动的唯一依据
    /// 其余所有步骤，制粉，装模具，热压，取模，还有加工的依据和要求都来自于计划表
    /// </summary>
    public class PMSPlanVHP
    {
        public Guid ID { get; set; }
        public int State { get; set; }
        public string Creator { get; set; }
        public DateTime? CreateTime { get; set; }

        public Guid OrderID { get; set; }

        public DateTime PlanDate { get; set; }
        public string VHPDeviceCode { get; set; }

        public string CurrentMold { get; set; }
        public double? CalculationDensity { get; set; }
        public double? MoldDiameter { get; set; }
        public double? Thickness { get; set; }
        public int Quantity { get; set; }

        public double? PowderWeight { get; set; }
        public string GrainSize { get; set; }

        public double? RoomTemperature { get; set; }
        public double? RoomHumidity { get; set; }

        public double? PreTemperature { get; set; }
        public double? PrePressure { get; set; }

        public double? Temperature { get; set; }
        public double? Pressure { get; set; }
        public double? Vaccum { get; set; }
        public double? KeepTempTime { get; set; }

        public string ProcessCode { get; set; }

        public string MillingRequirement { get; set; }
        public string FillingRequirement { get; set; }
        public string VHPRequirement { get; set; }
        public string MachineRequirement { get; set; }
        public string SpecialRequirement { get; set; }


        public string Remark { get; set; }

    }
}

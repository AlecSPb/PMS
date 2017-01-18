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
        //热压日期和热压设备，模具，共同唯一决定一个Plan
        //计划状态和创建信息，该计划是否有效
        public int State { get; set; }
        public string Creator { get; set; }
        public DateTime? CreateTime { get; set; }

        public Guid OrderID { get; set; }//对应的订单

        public DateTime PlanDate { get; set; }
        //热压设备
        public string VHPDeviceCode { get; set; }

        //模具和装料要求
        public string CurrentMold { get; set; }
        public double? CalculationDensity { get; set; }
        public double? MoldDiameter { get; set; }
        public double? Thickness { get; set; }
        public int Quantity { get; set; }
        public string FillRequirement { get; set; }
        //单片装料重量按照经验往往比上面计算得出的理论重量要多
        public double? PowderWeight { get; set; }
        public string GrainSize { get; set; }
        public string MillingRequirement { get; set; }

        //环境温度,湿度
        public double? RoomTemperature { get; set; }
        public double? RoomHumidity { get; set; }

        //预压力和预压温度
        public double? PreTemperature { get; set; }
        public double? PrePressure { get; set; }

        //实际压力，温度，真空度，保温时间
        public double? Temperature { get; set; }
        public double? Pressure { get; set; }
        public double? Vaccum { get; set; }
        public double? KeepTempTime { get; set; }

        //其他特殊要求
        public string FillingRequirement { get; set; }
        public string SpecialRequirement { get; set; }

        //后续步骤，回收，加工，保留，其他等等
        public string LaterProcess { get; set; }
        //后续步骤细节，如果是加工就是加工尺寸和要求
        public string LaterProcessDetails { get; set; }

        public string Remark { get; set; }
        
    }
}

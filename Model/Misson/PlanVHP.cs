using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    /// <summary>
    /// 热压计划
    /// 计划是按照热压日期，热压设备和热压模具来唯一确定的
    /// 这个热压计划只用来指导一次热压活动的唯一依据
    /// 其余所有步骤，制粉，装模具，热压，取模，还有加工的依据和要求都来自于计划表
    /// </summary>
    public class PlanVHP
    {
        public Guid ID { get; set; }
        //热压日期和热压设备，模具，共同唯一决定一个Plan
        public Guid OrderID { get; set; }//对应的订单

        public DateTime PlanDate { get; set; }
        //热压设备
        public string VHPDeviceCode { get; set; }
        //制粉相关
        public double CalculationDensity { get; set; }
        public double Thickness { get; set; }
        public int Quantity { get; set; }

        //单片装料重量按照经验往往比上面计算得出的理论重量要多
        public double PowderWeight { get; set; }
        public double GrainSize { get; set; }
        public string MillingRequirement { get; set; }


        //模具和装料要求
        public VHPMold CurrentMold { get; set; }
        public string FillRequirement { get; set; }

        //环境温度,湿度
        //传感器读入
        public string RoomTemperature { get; set; }
        public string RoomHumidity { get; set; }

        //预压力和预压温度
        public double PreTemperature { get; set; }
        public double PrePressure { get; set; }

        //实际压力，温度，真空度，保温时间
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double Vaccum { get; set; }
        public double KeepTempTime { get; set; }

        //其他特殊要求
        public string SpecialRequirement { get; set; }

        //装料要求
        public string FillingRequirement { get; set; }


        //后续步骤，回收，加工，保留，其他等等
        public string LaterProcess { get; set; }
        //后续步骤细节，如果是加工就是加工尺寸和要求
        public string LaterProcessDetails { get; set; }




        //计划状态和创建信息，该计划是否有效
        public int CurrentState { get; set; }

        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }

    }
}

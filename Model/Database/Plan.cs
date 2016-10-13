using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    /// <summary>
    /// 计划表
    /// 计划是按照热压日期，热压设备和热压模具来唯一确定的
    /// </summary>
    public class Plan
    {
        public Guid ID { get; set; }
        public DateTime PlanDate { get; set; }
        //模具和装料要求
        public VHPMold CurrentMold { get; set; }
        public string FillRequirement { get; set; }

        //热压设备和热压工艺信息
        public string VHPDeviceCode { get; set; }
        public string ProcessCode { get; set; }

        //环境温度,湿度
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


        //计划状态和创建信息，该计划是否有效
        public int CurrentState { get; set; }

        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    /// <summary>
    /// 计划表
    /// </summary>
    public class Plan
    {
        public Guid ID { get; set; }
        public DateTime PlanDate { get; set; }
        //模具和装料要求
        public string MoldCode { get; set; }
        public double MoldInnerDiameter { get; set; }
        public string FillRequirement { get; set; }

        //热压设备和热压工艺信息
        public string VHPDeviceCode { get; set; }
        public string ProcessCode { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double Vaccum { get; set; }
        public double KeepTempTime { get; set; }

        //计划状态和创建信息
        public int CurrentState { get; set; }

        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }

    }
}

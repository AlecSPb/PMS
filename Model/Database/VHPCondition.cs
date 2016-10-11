using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 热压工艺条件
    /// 包含热压所需要的所有参数
    /// </summary>
    public class VHPCondition
    {
        public Guid ID { get; set; }

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

    }
}

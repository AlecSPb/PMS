using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 用于存储自动记录的环境温度湿度
    /// </summary>
    public class EnvironmentInfo
    {
        public Guid ID { get; set; }
        public string Position { get; set; }
        public DateTime UpdateTime { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 热压过程的记录-项目
    /// </summary>
    public class RecordVHPItem
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CurrentTime { get; set; }

        public double PV1 { get; set; }
        public double PV2 { get; set; }
        public double PV3 { get; set; }
        public double SV { get; set; }

        public double Ton { get; set; }
        public double Vaccum { get; set; }
        public double Shift1 { get; set; }
        public double Shift2 { get; set; }

        public string ExtraInformation { get; set; }
    }
}

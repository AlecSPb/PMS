using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PMSDAL
{
    /// <summary>
    /// 热压过程的记录-项目
    /// </summary>
    public class RecordVHPHistory
    {
        [Key]
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CurrentTime { get; set; }
        public string State { get; set; }


        public double PV1 { get; set; }
        public double PV2 { get; set; }
        public double PV3 { get; set; }
        public double SV { get; set; }

        public double Ton { get; set; }
        public double Vaccum { get; set; }
        public double Shift1 { get; set; }
        public double Shift2 { get; set; }
        public double Omega { get; set; }
        public double WaterTemperatureOut { get; set; }
        public double WaterTemperatureIn { get; set; }

        public string ExtraInformation { get; set; }

        public Guid? PlanVHPID { get; set; }
        //操作者和操作时间
        [Key]
        public Guid HistoryID { get; set; }
        public string Operator { get; set; }
        public DateTime OperateTime { get; set; }
    }
}

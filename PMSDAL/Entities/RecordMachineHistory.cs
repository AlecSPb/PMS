using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace PMSDAL
{
    /// <summary>
    /// 机械加工记录
    /// </summary>
    public class RecordMachineHistory
    {
        public Guid ID { get; set; }
        //靶材信息
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string State { get; set; }
        public string VHPPlanLot { get; set; }
        public string Composition { get; set; }
        //加工要求
        public string Dimension { get; set; }
        public string ExtraRequirement { get; set; }

        //加工参数
        public double Diameter1 { get; set; }
        public double Diameter2 { get; set; }

        public double Thickness1 { get; set; }
        public double Thickness2 { get; set; }
        public double Thickness3 { get; set; }
        public double Thickness4 { get; set; }

        public string Defects { get; set; }

        //操作者和操作时间
        [Key]
        public Guid HistoryID { get; set; }
        public string Operator { get; set; }
        public DateTime OperateTime { get; set; }
    }
}

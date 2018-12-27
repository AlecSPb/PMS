using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace PMSDAL
{
    /// <summary>
    /// 材料需求表，由生产经理根据订单表新建而成
    /// </summary>
    public class MaterialNeedHistory
    {
        public Guid Id { get; set; }
        public string State { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }

        public string Composition { get; set; }
        public string Purity { get; set; }
        public double Weight { get; set; }
        public string SpecialNeeds { get; set; }
        public string PMINumber { get; set; }
        public string HowManyTargets { get; set; }

        //操作者和操作时间
        [Key]
        public Guid HistoryID { get; set; }
        public string Operator { get; set; }
        public DateTime OperateTime { get; set; }
    }
}

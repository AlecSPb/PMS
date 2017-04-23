using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PMSDAL
{
    /// <summary>
    /// 靶材产品
    /// </summary>
    public class RecordTestHistory
    {
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public string State { get; set; }//未审核，审核，作废
        public string TestType { get; set; }
        public string ProductID { get; set; }
        public string Composition { get; set; }
        public string CompositionAbbr { get; set; }
        public string PO { get; set; }
        public string Customer { get; set; }
        public string Dimension { get; set; }
        public string Density { get; set; }
        public string Weight { get; set; }
        public string Resistance { get; set; }
        public string CompositionXRF { get; set; }
        public string DimensionActual { get; set; }
        public string Defects { get; set; }
        public string Remark { get; set; }
        public string Sample { get; set; }
        public DateTime OrderDate { get; set; }
        //操作者和操作时间
        [Key]
        public Guid HistoryID { get; set; }
        public string Operator { get; set; }
        public DateTime OperateTime { get; set; }
    }
}

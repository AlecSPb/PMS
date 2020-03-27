using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 靶材信息
    /// </summary>
    public class RecordBonding
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string State { get; set; }
        public string CoverPlateNumber { get; set; }
        public string PlateType { get; set; }
        public string PlateLot { get; set; }//背板编号
        public int PlanBatchNumber { get; set; }
        public double WeldingRate { get; set; }

        public string TargetProductID { get; set; }//显示
        public string TargetComposition { get; set; }//显示
        public string TargetAbbr { get; set; }
        public string TargetCustomer { get; set; }
        public string TargetPO { get; set; }
        public string TargetPMINumber { get; set; }
        public string TargetWeight { get; set; }//显示
        public string TargetDimension { get; set; }//显示
        public string TargetDimensionActual { get; set; }
        public string TargetDefects { get; set; }
        public string TargetDetailRecord { get; set; }//复杂的信息写在这里
        public string Remark { get; set; }
    }
}

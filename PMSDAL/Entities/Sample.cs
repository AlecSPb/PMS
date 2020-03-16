using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 用于内部样品管理
    /// </summary>
    public class Sample : ModelBase
    {
        public string TrackingStage { get; set; }
        public string ProductID { get; set; }
        public string SampleID { get; set; }
        public string Composition { get; set; }
        public string PMINumber { get; set; }
        public string MoreInformation { get; set; }//重量等信息
        public string Customer { get; set; }
        public string OriginalRequirement { get; set; }
        public string TraceInformation { get; set; }//准备，检查，包装，发出
        public string ICPOES{ get; set; }//如果有测试报告就在这里填写
        public string GDMS{ get; set; }//如果有测试报告就在这里填写
        public string IGA { get; set; }
        public string Thermal { get; set; }
        public string Permittivity { get; set; }
        public string OtherTestResult { get; set; }
        public string Remark { get; set; }
        
        //2020-3-16补充
        public string PO { get; set; }
        public int Quantity { get; set; }
        public string Weight { get; set; }
        public string SampleType{ get; set; }
        public string SampleFor { get; set; }//备样目的

        //State 作废，正常

    }
}

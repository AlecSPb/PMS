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
        public string SampleType { get; set; }//客户，自分析，其他
        public string ProductID { get; set; }
        public string Composition { get; set; }
        public string PMINumber { get; set; }
        public string MoreInformation { get; set; }//重量等信息
        public string Customer { get; set; }
        public string OriginalRequirement { get; set; }
        public string TraceInformation { get; set; }//准备，检查，包装，发出
        public string TestResult{ get; set; }//如果有测试报告就在这里填写
        public string MoreTestResult { get; set; }
        public string Remark { get; set; }

        //State 作废，正常

    }
}

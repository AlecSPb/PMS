using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 用于内部样品管理
    /// </summary>
    [DataContract]
    public class DcSample : DcModelBase
    {
        [DataMember]
        public string SampleType { get; set; }//客户，自分析，其他
        [DataMember]
        public string ProductID { get; set; }
        [DataMember]
        public string Composition { get; set; }
        [DataMember]
        public string PMINumber { get; set; }
        [DataMember]
        public string MoreInformation { get; set; }//重量等信息
        [DataMember]
        public string Customer { get; set; }
        [DataMember]
        public string OrginalRequirment { get; set; }
        [DataMember]
        public string Process { get; set; }//准备，检查，包装，发出
        [DataMember]
        public string TestResult{ get; set; }//如果有测试报告就在这里填写
        [DataMember]
        public string MoreTestResult { get; set; }
        [DataMember]
        public string Remark { get; set; }

        //State 作废，正常

    }
}

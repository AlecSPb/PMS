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
        public string TrackingStage { get; set; }
        [DataMember]
        public string ProductID { get; set; }
        [DataMember]
        public string SampleID { get; set; }
        [DataMember]
        public string Composition { get; set; }
        [DataMember]
        public string PMINumber { get; set; }
        [DataMember]
        public string MoreInformation { get; set; }//重量等信息
        [DataMember]
        public string Customer { get; set; }
        [DataMember]
        public string OriginalRequirement { get; set; }
        [DataMember]
        public string OriginalRequirementRemark { get; set; }
        [DataMember]
        public string TraceInformation { get; set; }//准备，检查，包装，发出
        [DataMember]
        public string ICPOES { get; set; }//如果有测试报告就在这里填写
        [DataMember]
        public string GDMS { get; set; }//如果有测试报告就在这里填写
        [DataMember]
        public string IGA { get; set; }
        [DataMember]
        public string Thermal { get; set; }
        [DataMember]
        public string Permittivity { get; set; }
        [DataMember]
        public string OtherTestResult { get; set; }
        [DataMember]
        public string Remark { get; set; }

        //2020-3-16补充
        [DataMember]
        public string PO { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public string Weight { get; set; }
        [DataMember]
        public string SampleType { get; set; }
        [DataMember]
        public string SampleFor { get; set; }

        //State 作废，正常

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcOrder
    {
        //基本信息
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string PO { get; set; }
        [DataMember]
        public string PMINumber { get; set; }
        [DataMember]
        public string CompositionStandard { get; set; }
        [DataMember]
        public string CompositionOriginal { get; set; }
        [DataMember]
        public string CompositionAbbr { get; set; }
        [DataMember]
        public string ProductType { get; set; }
        [DataMember]
        public string Purity { get; set; }
        [DataMember]
        public double Quantity { get; set; }
        [DataMember]
        public string QuantityUnit { get; set; }
        [DataMember]
        public string Dimension { get; set; }
        [DataMember]
        public string DimensionDetails { get; set; }
        [DataMember]
        public string SampleNeed { get; set; }
        [DataMember]
        public DateTime DeadLine { get; set; }
        [DataMember]
        public string MinimumAcceptDefect { get; set; }
        [DataMember]
        public string Remark { get; set; }



        //状态部分
        [DataMember]
        public string Priority { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string StateRemark { get; set; }

        //热压指数
        public double ProductionIndex { get; set; }

        //创建者和审核部分
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string Creator { get; set; }

        [DataMember]
        public DateTime ReviewTime { get; set; }
        [DataMember]
        public string Reviewer{ get; set; }

        //决策部分
        [DataMember]
        public string PolicyType { get; set; }
        [DataMember]
        public DateTime FinishTime { get; set; }
    }
}

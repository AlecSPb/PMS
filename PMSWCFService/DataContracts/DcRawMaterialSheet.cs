using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 三杰记录初级原料的库存，用作追踪初级原料的数据表
    /// </summary>
    [DataContract]
    public class DcRawMaterialSheet : DcModelBase
    {
        [DataMember]
        public string Lot { get; set; }
        [DataMember]
        public string Composition { get; set; }
        [DataMember]
        public string Supplier { get; set; }
        [DataMember]
        public double Weight { get; set; }//kg
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public DateTime StoreTime { get; set; }
        //State 作废 [在库] 耗尽 取消

        [DataMember]
        public bool IsSampleTaking { get; set; }
        [DataMember]
        public DateTime SampleTakingTime { get; set; }
        [DataMember]
        public string GDMS { get; set; }
        [DataMember]
        public string ICPOES { get; set; }
    }
}

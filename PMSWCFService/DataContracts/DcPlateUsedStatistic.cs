using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 背板使用统计模型
    /// </summary>
    [DataContract]
    public class DcPlateUsedStatistic
    {
        [DataMember]
        public string PlateLot { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
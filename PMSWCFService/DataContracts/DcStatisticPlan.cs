using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 统计数据集合
    /// </summary>
    [DataContract]
    public class DcStatisticPlan
    {

        [DataMember]
        public int AllPlanCount { get; set; }
        [DataMember]
        public DeviceUsedTime DeviceCount { get; set; }

    }

    [DataContract]
    public class DeviceUsedTime
    {
        [DataMember]
        public string DeviceCode { get; set; }
        [DataMember]
        public int UseCount { get; set; }
    }
}
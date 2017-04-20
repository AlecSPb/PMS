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
    public class DcStatisticDelivery
    {

        //Order
        [DataMember]
        public int AllDeliveryCount { get; set; }
    }
}
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
    public class DcOrderStatistic
    {

        //Order
        [DataMember]
        public int AllOrderCount { get; set; }
        [DataMember]
        public int UnFinishedOrderCount { get; ; set; }
        [DataMember]
        public int FinishedOrderCount { get; set; }



        //Misson
        [DataMember]
        public int AllMissonCount { get; set; }
        [DataMember]
        public int UnCompletedMissonCount { get; set; }
        [DataMember]
        public int CompletedMissonCount { get; set; }

    }
}
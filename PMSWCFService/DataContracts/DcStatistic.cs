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
    public class DcStatistic
    {

        //Order


        //Misson
        [DataMember]
        public int UnCompletedMisson { get; set; }
        [DataMember]
        public int CompletedMisson { get; set; }

    }
}
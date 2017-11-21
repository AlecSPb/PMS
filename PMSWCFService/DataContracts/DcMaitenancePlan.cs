using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcMaintenancePlan
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public string CreateTime { get; set; }
        [DataMember]
        public string State { get; set; }//Run,Stop,Paused
        [DataMember]
        public string Device { get; set; }//设备
        [DataMember]
        public string Item { get; set; }//部件
        [DataMember]
        public string Grade { get; set; }//保养级别
        [DataMember]
        public int Interval { get; set; }//保养周期，单位天
        [DataMember]
        public string Content { get; set; }//保养具体内容
        [DataMember]
        public string Remark { get; set; }
    }
}

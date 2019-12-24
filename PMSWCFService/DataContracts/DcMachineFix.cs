using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcMachineFix
    {
        [DataMember]
        public string FixType { get; set; }//保养还是维修
        [DataMember]
        public string DeviceName { get; set; }//设备
        [DataMember]
        public string PartName { get; set; }//部件
        [DataMember]
        public string EventDescription { get; set; }//现象，原因
        [DataMember]
        public string FixMeasure { get; set; }//处理方案和结果
        [DataMember]
        public DateTime StartTime { get; set; }
        [DataMember]
        public DateTime EndTime { get; set; }
        [DataMember]
        public string Remark { get; set; }
    }
}
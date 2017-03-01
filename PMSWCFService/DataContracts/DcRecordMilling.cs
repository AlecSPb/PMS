using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 制粉记录
    /// </summary>
    [DataContract]
    public class DcRecordMilling
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public string CreateTime { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public Guid PlanID { get; set; }//Foreign Key
        //需要记录的信息
        [DataMember]
        public string RawMaterial { get; set; }
        [DataMember]
        public string FromWho { get; set; }//MaterialSource
        [DataMember]
        public string ExtraInformation { get; set; }
        [DataMember]
        public string MillingTool { get; set; }
        [DataMember]
        public string GasProtection { get; set; }
        [DataMember]
        public double MaterialIn { get; set; }
        [DataMember]
        public double MaterialOut { get; set; }
        [DataMember]
        public double MaterialRemain { get; set; }

    }
}

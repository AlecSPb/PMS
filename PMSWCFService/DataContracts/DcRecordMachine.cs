using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 机械加工记录
    /// </summary>
    [DataContract]
    public class DcRecordMachine
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        //靶材信息
        public string Creator { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string VHPPlanLot { get; set; }
        [DataMember]
        public string Composition { get; set; }
        [DataMember]
        public string Dimension { get; set; }
        [DataMember]
        public string ExtraRequirement { get; set; }

        //加工参数
        [DataMember]
        public double Diameter1 { get; set; }
        [DataMember]
        public double Diameter2 { get; set; }
        [DataMember]
        public double Thickness1 { get; set; }
        [DataMember]
        public double Thickness2 { get; set; }
        [DataMember]
        public double Thickness3 { get; set; }
        [DataMember]
        public double Thickness4 { get; set; }
        [DataMember]
        public string Defects { get; set; }
    }
}

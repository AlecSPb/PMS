using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 取模记录
    /// </summary>
    [DataContract]
    public class DcRecordDeMold
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
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
        public string MoveOutTemperature { get; set; }
        [DataMember]
        public string TakeOutTemperature { get; set; }
        [DataMember]
        public string ExtraInformation { get; set; }

        [DataMember]
        public double RoughTargetWeight { get; set; }
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
        public string WithExtraThickness { get; set; }//有额外的厚度

    }
}

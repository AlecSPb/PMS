using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 热压过程的记录-项目
    /// </summary>
    [DataContract]
    public class DcRecordVHPItem
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime CurrentTime { get; set; }
        [DataMember]
        public double PV1 { get; set; }
        [DataMember]
        public double PV2 { get; set; }
        [DataMember]
        public double PV3 { get; set; }
        [DataMember]
        public double SV { get; set; }
        [DataMember]
        public double Ton { get; set; }
        [DataMember]
        public double Vaccum { get; set; }
        [DataMember]
        public double Shift1 { get; set; }
        [DataMember]
        public double Shift2 { get; set; }
        [DataMember]
        public double Omega { get; set; }
        [DataMember]
        public double WaterTemperatureOut { get; set; }
        [DataMember]
        public double WaterTemperatureIn { get; set; }
        [DataMember]
        public string ExtraInformation { get; set; }

    }
}

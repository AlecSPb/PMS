using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 热压过程当中的记录
    /// </summary>
    [DataContract]
    public class DcRecordVHP
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public Guid PlanID { get; set; }
        [DataMember]
        public DateTime PlanDate { get; set; }//161210-M
        [DataMember]
        public string Composition { get; set; }
        [DataMember]
        public string MoldType { get; set; }

        [DataMember]
        public string VHPDeviceCode { get; set; }
        [DataMember]
        public double MoldDiameter { get; set; }
        //预压力和预压温度
        [DataMember]
        public double PreTemperature { get; set; }
        [DataMember]
        public double PrePressure { get; set; }

        //实际压力，温度，真空度，保温时间
        [DataMember]
        public double Temperature { get; set; }
        [DataMember]
        public double Pressure { get; set; }
        [DataMember]
        public double Vaccum { get; set; }
        [DataMember]
        public double KeepTempTime { get; set; }
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public virtual List<DcRecordVHPItem> RecordVHPItems { get; set; }
    }
}

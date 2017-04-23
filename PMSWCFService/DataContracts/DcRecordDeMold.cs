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
        public string PlanType { get; set; }
        [DataMember]
        public string VHPPlanLot { get; set; }
        [DataMember]
        public string Composition { get; set; }
        [DataMember]
        public string BlankDimension { get; set; }
        [DataMember]
        public double CalculationDensity { get; set; }
        [DataMember]
        public double Density { get; set; }
        [DataMember]
        public double RatioDensity { get; set; }
        [DataMember]
        public string Temperature1 { get; set; }
        [DataMember]
        public string Temperature2 { get; set; }

        [DataMember]
        public string DeMoldType { get; set; }
        [DataMember]
        public double Weight { get; set; }
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
        public string Remark { get; set; }

    }
}

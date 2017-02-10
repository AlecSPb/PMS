using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcPlanVHP
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public int State { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime? CreateTime { get; set; }
        [DataMember]
        public Guid OrderID { get; set; }
        [DataMember]
        public DateTime PlanDate { get; set; }
        [DataMember]
        public string VHPDeviceCode { get; set; }
        [DataMember]
        public string CurrentMold { get; set; }
        [DataMember]
        public double? CalculationDensity { get; set; }
        [DataMember]
        public double? MoldDiameter { get; set; }
        [DataMember]
        public double? Thickness { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public double? PowderWeight { get; set; }
        [DataMember]
        public string GrainSize { get; set; }
        [DataMember]
        public double? RoomTemperature { get; set; }
        [DataMember]
        public double? RoomHumidity { get; set; }
        [DataMember]
        public double? PreTemperature { get; set; }
        [DataMember]
        public double? PrePressure { get; set; }
        [DataMember]
        public double? Temperature { get; set; }
        [DataMember]
        public double? Pressure { get; set; }
        [DataMember]
        public double? Vaccum { get; set; }
        [DataMember]
        public double? KeepTempTime { get; set; }
        [DataMember]
        public string ProcessCode { get; set; }
        [DataMember]
        public string MillingRequirement { get; set; }
        [DataMember]
        public string FillingRequirement { get; set; }
        [DataMember]
        public string VHPRequirement { get; set; }
        [DataMember]
        public string MachineRequirement { get; set; }
        [DataMember]
        public string SpecialRequirement { get; set; }
        [DataMember]
        public string Remark { get; set; }

        public  DcOrder Order { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcMissonWithPlan
    {
        //Order or Misson
        [DataMember]
        public Guid OrderID { get; set; }

        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string PO { get; set; }
        [DataMember]
        public string PMIWorkingNumber { get; set; }
        [DataMember]
        public string CompositionStandard { get; set; }
        [DataMember]
        public string CompositionOriginal { get; set; }
        [DataMember]
        public string CompositoinAbbr { get; set; }
        [DataMember]
        public string ProductType { get; set; }
        [DataMember]
        public string Purity { get; set; }
        [DataMember]
        public double Quantity { get; set; }
        [DataMember]
        public string QuantityUnit { get; set; }
        [DataMember]
        public string Dimension { get; set; }
        [DataMember]
        public string DimensionDetails { get; set; }
        [DataMember]
        public string SampleNeed { get; set; }
        [DataMember]
        public DateTime DeadLine { get; set; }
        [DataMember]
        public string MinimumAcceptDefect { get; set; }
        [DataMember]
        public string OrderRemark { get; set; }

        //VHP Plan
        [DataMember]
        public Guid PlanID { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime? CreateTime { get; set; }
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
        public int TargetQuantity { get; set; }
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
        public string VHPPlanRemark { get; set; }

    }
}
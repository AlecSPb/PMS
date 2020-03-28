using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcOutputSpecialFor230Model
    {
        [DataMember]
        public string ProductID { get; set; }
        [DataMember]
        public string Customer { get; set; }
        [DataMember]
        public string Composition { get; set; }
        [DataMember]
        public string CompositionAbbr { get; set; }

        [DataMember]
        public string Dimension { get; set; }
        [DataMember]
        public string DimensionActual { get; set; }
        [DataMember]
        public string Weight { get; set; }
        [DataMember]
        public string Density { get; set; }
        [DataMember]
        public string Resistance { get; set; }
        [DataMember]
        public string PlateType { get; set; }
        [DataMember]
        public string PlateLot { get; set; }
        [DataMember]
        public DateTime BondCreateTime { get; set; }
        [DataMember]
        public double WeldingRate { get; set; }
        [DataMember]
        public DateTime DeliveryCreateTime { get; set; }
        [DataMember]
        public string Position { get; set; }
        [DataMember]
        public string CompositionXRF { get; set; }
    }
}
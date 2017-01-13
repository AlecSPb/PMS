using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcMaterialOrderItem
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public int State { get; set; }
        [DataMember]
        public string PMIWorkNumber { get; set; }
        [DataMember]
        public string Composition { get; set; }
        [DataMember]
        public string Purity { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string ProvideRawMaterial { get; set; }
        [DataMember]
        public DateTime DeliveryDate { get; set; }
        [DataMember]
        public double UnitPrice { get; set; }
        [DataMember]
        public double Weight { get; set; }

    }
}

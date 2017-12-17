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
        public string State { get; set; }
        [DataMember]
        public string OrderItemNumber { get; set; }
        [DataMember]
        public string PMINumber { get; set; }
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
        [DataMember]
        public string Priority { get; set; }
        [DataMember]
        public Guid MaterialOrderID { get; set; }
        //2017-12-17
        [DataMember]
        public string SJIngredient { get; set; }
    }
}

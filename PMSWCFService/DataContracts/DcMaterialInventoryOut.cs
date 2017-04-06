using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class PMSMaterialInventoryOut
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string Composition { get; set; }
        [DataMember]
        public string Purity { get; set; }
        [DataMember]
        public string Supplier { get; set; }
        [DataMember]
        public double Weight { get; set; }
        [DataMember]
        public string Remark { get; set; }
    }
}

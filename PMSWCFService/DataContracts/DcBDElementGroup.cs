using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class BDElementGroup
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public string CreateTime { get; set; }
    }
}

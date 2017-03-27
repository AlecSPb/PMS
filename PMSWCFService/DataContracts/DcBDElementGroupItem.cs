using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcBDElementGroupItem
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public Guid GroupElementID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int GroupNumber { get; set; }
        [DataMember]
        public double MolWeight { get; set; }
        [DataMember]
        public double At { get; set; }
    }
}

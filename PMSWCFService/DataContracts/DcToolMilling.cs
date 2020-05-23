using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcToolMilling
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string Creator { get; set; }

        [DataMember]
        public string State { get; set; }


        [DataMember]
        public int BoxNumber { get; set; }
        [DataMember]
        public int ToolNumber { get; set; }
        [DataMember]
        public string CompositionAbbr { get; set; }

        [DataMember]
        public string Remark { get; set; }
    }
}

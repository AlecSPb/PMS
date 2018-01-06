using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcEnvironmentInfo
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Position { get; set; }
        [DataMember]
        public DateTime UpdateTime { get; set; }
        [DataMember]
        public double Temperature { get; set; }
        [DataMember]
        public double Humidity { get; set; }
    }
}

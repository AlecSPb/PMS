using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcVHPDevice
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string CodeName { get; set; }
        [DataMember]
        public string DeviceInformation { get; set; }
        [DataMember]
        public double HighestTemperature { get; set; }
        [DataMember]
        public double HighestPressure { get; set; }
        [DataMember]
        public double HighestDiameter { get; set; }
        [DataMember]
        public int State { get; set; }
        [DataMember]
        public string Manufacturer { get; set; }//制造商
        [DataMember]
        public string ReceiveTime { get; set; }//接受时间
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }


    }
}

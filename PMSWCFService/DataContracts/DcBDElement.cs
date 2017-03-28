using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 存储元素周期表
    /// </summary>
    [DataContract]
    public class DcBDElement
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int AtomicNumber { get; set; }
        [DataMember]
        public double MolWeight { get; set; }
        [DataMember]
        public double Density { get; set; }
        [DataMember]
        public string MeltingPoint { get; set; }
        [DataMember]
        public string BoilingPoint { get; set; }
    }
}

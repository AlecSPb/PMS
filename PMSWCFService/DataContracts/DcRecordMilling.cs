using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 制粉记录
    /// </summary>
    [DataContract]
    public class DcRecordMilling
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public Guid PlanID { get; set; }//Foreign Key
        [DataMember]
        //需要记录的信息
        public string Composition { get; set; }
        [DataMember]
        public string MaterialSource { get; set; }//MaterialSource
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public string MillingTool { get; set; }
        [DataMember]
        public string GasProtection { get; set; }
        [DataMember]
        public double WeightIn { get; set; }
        [DataMember]
        public double WeightOut { get; set; }
        [DataMember]
        public double WeightRemain { get; set; }

    }
}

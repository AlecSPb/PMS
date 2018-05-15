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
        public string VHPPlanLot { get; set; }
        [DataMember]
        public int PlanBatchNumber { get; set; }
        [DataMember]
        public double RoomTemperature { get; set; }
        [DataMember]
        public double RoomHumidity { get; set; }
        [DataMember]
        public string PMINumber { get; set; }
        [DataMember]
        //需要记录的信息
        public string Composition { get; set; }
        [DataMember]
        public string MaterialType { get; set; }
        [DataMember]
        public string MaterialSource { get; set; }//MaterialSource
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public string MillingTool { get; set; }
        [DataMember]
        public string GasProtection { get; set; }
        [DataMember]
        public string GrainSize { get; set; }
        [DataMember]
        public double WeightIn { get; set; }
        [DataMember]
        public double WeightOut { get; set; }
        [DataMember]
        public double WeightRemain { get; set; }
        [DataMember]
        public double Ratio { get; set; }
        [DataMember]
        public string MillingTime { get; set; }
        [DataMember]
        public string Oxygen { get; set; }
        [DataMember]
        public string Water { get; set; }
        //2017-12-15
        [DataMember]
        public string MeltingPoint { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcRecordBonding
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
        public string CoverPlateNumber { get; set; }
        [DataMember]
        public string PlateType { get; set; }
        [DataMember]
        public string PlateLot { get; set; }//背板编号
        [DataMember]
        public int PlanBatchNumber { get; set; }
        [DataMember]
        public double WeldingRate { get; set; }

        [DataMember]
        public string TargetProductID { get; set; }//显示
        [DataMember]
        public string TargetComposition { get; set; }//显示
        [DataMember]
        public string TargetAbbr { get; set; }
        [DataMember]
        public string TargetCustomer { get; set; }
        [DataMember]
        public string TargetPO { get; set; }
        [DataMember]
        public string TargetPMINumber { get; set; }
        [DataMember]
        public string TargetWeight { get; set; }//显示
        [DataMember]
        public string TargetDimension { get; set; }//显示
        [DataMember]
        public string TargetDimensionActual { get; set; }
        [DataMember]
        public string TargetDefects { get; set; }
        [DataMember]
        public string TargetDetailRecord { get; set; }//复杂的信息写在这里
        [DataMember]
        public string Remark { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 存放查询到的所有综合信息
    /// </summary>
    [DataContract]
    public class DcComplexQueryResult
    {
        public DcComplexQueryResult()
        {
            PlanVHPWithMissons = new List<DcPlanWithMisson>();
            MaterialInventoryIns = new List<DcMaterialInventoryIn>();
            MaterialInventoryOuts = new List<DcMaterialInventoryOut>();
            MaterialOrderItemExtras = new List<DcMaterialOrderItemExtra>();
            RecordMillings = new List<DcRecordMilling>();
            RecordVHPs = new List<DcRecordVHP>();
            RecordDemolds = new List<DcRecordDeMold>();
            RecordMachines = new List<DcRecordMachine>();
            RecordTests = new List<DcRecordTest>();
        }
        [DataMember]
        public List<DcPlanWithMisson> PlanVHPWithMissons { get; set; }
        [DataMember]
        public List<DcMaterialOrderItemExtra> MaterialOrderItemExtras { get; set; }

        [DataMember]
        public List<DcMaterialInventoryIn> MaterialInventoryIns { get; set; }
        [DataMember]
        public List<DcMaterialInventoryOut> MaterialInventoryOuts { get; set; }
        [DataMember]
        public List<DcRecordMilling> RecordMillings { get; set; }
        [DataMember]
        public List<DcRecordVHP> RecordVHPs { get; set; }
        [DataMember]
        public List<DcRecordDeMold> RecordDemolds { get; set; }
        [DataMember]
        public List<DcRecordMachine> RecordMachines { get; set; }
        [DataMember]
        public List<DcRecordTest> RecordTests { get; set; }

    }
}
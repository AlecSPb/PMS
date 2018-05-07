using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts.Model
{
    [DataContract]
    public class PMSReadyOutMaterialModel
    {
        [DataMember]
        public DcMaterialInventoryIn MaterialInModel { get; set; }
        [DataMember]
        public DcMaterialInventoryOut MaterialOutModel { get; set; }
        [DataMember]
        public DcRecordMilling RecordMillingModel { get; set; }
    }
}
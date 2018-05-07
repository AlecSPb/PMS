using PMSDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts.Model
{
    [DataContract]
    public class PMSReadyOutMaterialModelRaw
    {
        [DataMember]
        public MaterialInventoryIn MaterialInModel { get; set; }
        [DataMember]
        public MaterialInventoryOut MaterialOutModel { get; set; }
        [DataMember]
        public RecordMilling RecordMillingModel { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// Drawing
    ///存放图纸
    /// </summary>
    [DataContract]
    public class DcDrawing : DcModelBase
    {
        [DataMember]
        public string DrawingName { get; set; }
        [DataMember]
        public string DrawingType { get; set; }
        [DataMember]
        public string Customer { get; set; }
        [DataMember]
        public string MainDimension { get; set; }
        [DataMember]
        public string ExtraDimension { get; set; }
        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public Guid DrawID { get; set; }

    }
}

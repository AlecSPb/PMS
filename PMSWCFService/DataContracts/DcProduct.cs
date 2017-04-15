using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcProduct
    {
      [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string ProductType { get; set; }//裸靶 or Bonding or其他
        [DataMember]
        public string ProductID { get; set; }
        [DataMember]
        public string Composition { get; set; }
        [DataMember]
        public string Abbr { get; set; }
        [DataMember]
        public string Customer { get; set; }
        [DataMember]
        public string PO { get; set; }
        [DataMember]
        public string Weight { get; set; }
        [DataMember]
        public string Dimension { get; set; }//显示
        [DataMember]
        public string DimensionActual { get; set; }
        [DataMember]
        public string Defects { get; set; }
        [DataMember]
        public string Remark { get; set; }//复杂的信息写在这里
        [DataMember]
        public string Position { get; set; }//入库库房编号，位置编号，unknown，成品库房 产品架A




    }
}

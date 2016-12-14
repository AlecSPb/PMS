using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Product
{
    public class ProductSample
    {
        public Guid ID { get; set; }
        public string ProductID { get; set; }
        public string Composition { get; set; }
        public string CompositionAbbr { get; set; }
        public string PO { get; set; }
        public string Customer { get; set; }
        public string Weight1{ get; set; }
        public string Weight2 { get; set; }
        public string Weight3 { get; set; }
        public string Weight4 { get; set; }
        public string ForTarget { get; set; }

        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public int State { get; set; }//未审核，审核，作废
    }
}

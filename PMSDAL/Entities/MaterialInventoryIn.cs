using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class MaterialInventoryIn
    {
        public Guid Id { get; set; }
        public string State { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string MaterialLot { get; set; }
        public string PMINumber { get; set; }
        public string Composition { get; set; }
        public string Purity { get; set; }
        public string Supplier { get; set; }
        public double Weight { get; set; }
        public string Remark { get; set; }
        public string QuickRemark { get; set; }//部分出库临时记录

        public string MaterialSource { get; set; }
        public string SupplierPO { get; set; }//

        public string GDMS { get; set; }
        public string ICPOES { get; set; }

    }
}

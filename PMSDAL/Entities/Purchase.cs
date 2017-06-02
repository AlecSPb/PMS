using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class Purchase
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public string State { get; set; }//取消，未审核，审核通过，已发货

        public string ItemName { get; set; }
        public string Specification { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public string NeedPerson { get; set; }
        public string PurchasePerson { get; set; }
        public double Cost { get; set; }
        public string Remark { get; set; }

    }
}

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
        public string State { get; set; }

        public string ItemType { get; set; }
        public string ItemName { get; set; }
        public string Specification { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public double Cost { get; set; }
        public string NeedDepartment { get; set; }
        public string PurchasePerson { get; set; }
        public string Remark { get; set; }

    }
}

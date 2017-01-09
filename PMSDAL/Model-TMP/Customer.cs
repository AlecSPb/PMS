using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL.Model
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string ContractPerson { get; set; }
        public string CellPhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
    }
}

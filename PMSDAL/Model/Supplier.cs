using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Developer:xs.zhou@outlook.com
    CreateTime:2016/4/25 11:43:04
*/
namespace PMSDAL.Model
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string CellPhone { get; set; }
        public string Fax { get; set; }
        public string QQ { get; set; }
        public string TheirProducts { get; set; }//他们的产品
        public string Email { get; set; }
        public string OtherContact { get; set; }//其他联系方式
        public string Remark { get; set; }
    }
}

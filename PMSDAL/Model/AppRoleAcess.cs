using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Developer:xs.zhou@outlook.com
    CreateTime:2016/4/25 10:50:18
*/
namespace PMSDAL.Model
{
    public class AppRoleAcess
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid AccessId { get; set; }
    }
}

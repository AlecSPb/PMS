using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL.Model
{
    public class AppUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Password { get; set; }
        //设置用户账户状态，有效，无效
        public string UserState { get; set; }

        //每个用户只能有一个角色
        public Guid RoleId { get; set; }
    }
}

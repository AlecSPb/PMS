using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL.Model
{
    public class AppUser
    {
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string RealName { get; set; }

        public DateTime CreateTime { get; set; }
        [Required]
        public string Password { get; set; }
        //设置用户账户状态，有效，无效
        [Required]
        public string State { get; set; }

        //每个用户只能有一个角色
        [Required]
        public Guid RoleId { get; set; }
    }
}

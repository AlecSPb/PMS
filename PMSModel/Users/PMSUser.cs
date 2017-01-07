using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSModel
{
    public class PMSUser
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }//密码必须MD5并加盐
        public DateTime CreateTime { get; set; }
        public int CurrentState { get; set; }//当前账户是否有效

        public string RealName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        //Navigation
        public virtual PMSRole Role { get; set; }
    }
}

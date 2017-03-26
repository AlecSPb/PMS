using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.UserService;

namespace PMSClient.Helpers
{
    /// <summary>
    /// 存储和处理客户端所需要的用户信息
    /// </summary>
    public class UserControl
    {
        public DcUser CurrentUser { get; set; }
        public DcUserRole CurrentUserRole { get; set; }
        public List<DcUserAccess> CurrentAccesses { get; set; }


    }
}

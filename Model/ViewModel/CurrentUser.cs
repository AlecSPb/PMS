using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 客户端里的当前用户
    /// 需要存储的信息
    /// </summary>
    public class CurrentUser
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public bool IsLoginValid { get; set; }//确保用户验证有效
        public DateTime LoginTime { get; set; }


        //权限控制部分
        public string CurrentRole { get; set; }
        //存储该用户所拥有的所有权限，检查一个用户是否拥有该权限的时候直接查找该list即可
        public List<string> CurrentAccess { get; set; }


    }
}

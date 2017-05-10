using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationChecker
{
    public static  class Checker
    {

        public static void CheckIfCanRun()
        {
            //最后使用期限 To Protect My Self
            var untilTime = new DateTime(2018, 1, 1);
            if (DateTime.Now>untilTime)
            {
                throw new Exception("服务器错误，代码610324,请联系管理员");
            }
        }
    }
}

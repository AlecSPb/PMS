using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Helpers
{
    public static class AccessHelper
    {
        public static bool IsAdmin()
        {
            bool isAdmin = true;
            if (PMSHelper.CurrentSession.CurrentUserRole?.GroupName == "管理员")
            {
                isAdmin = true;
            }
            else
            {
                isAdmin = false;
            }

            return isAdmin;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient
{
    /// <summary>
    /// 显示状态信息
    /// </summary>
    public static class PMSStatus
    {
        public static void SaveOK()
        {
            NavigationService.ShowStatusMessage("保存成功");
        }
        public static void SaveNotOK()
        {
            NavigationService.ShowStatusMessage("保存不成功");
        }
        public static void NewItem()
        {
            NavigationService.ShowStatusMessage("开始新建");
        }
        public static void EditItem()
        {
            NavigationService.ShowStatusMessage("开始编辑");
        }
        public static void DeleteItem()
        {
            NavigationService.ShowStatusMessage("准备作废");
        }

        public static void LogInOK()
        {
            NavigationService.ShowStatusMessage("登录成功");
        }
        public static void LogInNotOK()
        {
            NavigationService.ShowStatusMessage("登录不成功");
        }
        public static void LogOutOK()
        {
            NavigationService.ShowStatusMessage("注销成功");
        }


    }
}

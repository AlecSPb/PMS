using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;

namespace PMSClientViewModel
{
    public static class NavigationService
    {
        /// <summary>
        /// 导航到某个视图
        /// </summary>
        /// <param name="obj">视图枚举</param>
        public static void GoTo(MsgObject obj)
        {
            Messenger.Default.Send<MsgObject>(obj, NavigationToken.Navigate);
        }
        /// <summary>
        /// 显示状态信息到MainWindow
        /// </summary>
        /// <param name="msg">状态信息字符串</param>
        public static void ShowStateMessage(string msg = "状态信息")
        {
            Messenger.Default.Send<string>(msg, PMSCommon.NavigationToken.StateMessage);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="refreshtoken">刷新Token</param>
        public static void Refresh(VToken refreshtoken)
        {
            Messenger.Default.Send<MsgObject>(null, refreshtoken);
        }

    }
}

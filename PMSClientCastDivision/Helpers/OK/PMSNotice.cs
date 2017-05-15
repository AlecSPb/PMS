using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.SanjieService;
using System.Timers;
using PMSClient.CustomControls;

namespace PMSClient
{
    /// <summary>
    /// 用到的各类通知
    /// </summary>
    public static class PMSNotice
    {
        public static string NoticeMessage()
        {
            StringBuilder sb = new StringBuilder();

            #region 通知检测区域
            sb.AppendLine("有新订单了");
            sb.AppendLine("有新生产任务了");
            sb.AppendLine("有新材料需求了");
            sb.AppendLine("有新绑定计划了");
            sb.AppendLine("有发货计划了");
            return sb.ToString();
        }


        public static void ShowNoticeWindow()
        {
            #endregion
            string notice = NoticeMessage();
            if (string.IsNullOrEmpty(notice))
            {
                return;
            }
            NoticeWindow noticeWindow = new NoticeWindow();
            noticeWindow.NoticeMessage = notice;
            noticeWindow.ShowDialog();
        }
    }
}

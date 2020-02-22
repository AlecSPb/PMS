using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XSHelper.Helpers
{
    /// <summary>
    /// 消息框帮助类
    /// </summary>
    public class MessageboxHelper
    {
        /// <summary>
        /// 显示基本信息
        /// </summary>
        /// <param name="content"></param>
        /// <param name="caption"></param>
        public void ShowInfo(string content, string caption = "信息")
        {
            MessageBox.Show(content, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool ShowYesNo(string content, string caption = "请问")
        {
            var result = MessageBox.Show(content, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }

        public void ShowError(string content, string caption = "错误")
        {
            MessageBox.Show(content, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void ShowStop(string content, string caption = "致命错误")
        {
            MessageBox.Show(content, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        public void ShowWarning(string content, string caption = "警告")
        {
            MessageBox.Show(content, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

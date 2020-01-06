using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonHelper
{
    /// <summary>
    /// 公共帮助类
    /// </summary>
    public class BasicHelper
    {
        /// <summary>
        /// 在WinForm中获取文件夹位置
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public string GetDirectoryPath(string description)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = description;
            dialog.ShowNewFolderButton = true;
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            else
            {
                return "";
            }
        }

        #region 状态消息处理
        public event EventHandler<string> UpdateTextBox;
        private StringBuilder statusMessage = new StringBuilder();
        public void InsertStatus(string msg)
        {
            statusMessage.Insert(0, $"{msg}\r\n");
            InvokeUpdateTextBox(statusMessage.ToString());
        }
        public void AppendStatus(string msg)
        {
            statusMessage.AppendLine(msg);
            InvokeUpdateTextBox(statusMessage.ToString());
        }

        private void InvokeUpdateTextBox(string statusMessage)
        {
            if (UpdateTextBox != null)
                UpdateTextBox.Invoke(this, statusMessage);
        }

        public void ClearStatus()
        {
            statusMessage.Clear();
        }
        #endregion

    }
}

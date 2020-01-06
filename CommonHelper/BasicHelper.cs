using System;
using System.Collections.Generic;
using System.IO;
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
        #region 文件处理
        //返回当前文件夹路径
        public string GetCurrentFolderPath()
        {
            return Environment.CurrentDirectory;
        }
        /// <summary>
        /// 返回桌面路径
        /// </summary>
        /// <returns></returns>
        public string GetDesktopPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }
        /// <summary>
        /// 获取当前时间文件名，精确到秒
        /// </summary>
        /// <param name="extensionName"></param>
        /// <returns></returns>
        public string GetDateTimeFileName(string extensionName = "docx")
        {
            return $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.{extensionName}";
        }
        /// <summary>
        /// 获取完整文件名
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public string GetFullFileName(string folder, string file)
        {
            return Path.Combine(folder, file);
        }
        #endregion

        #region 对话框
        /// <summary>
        /// 在WinForm中获取文件夹位置
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public PathParameter GetDirectoryPath(string description)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = description;
            dialog.ShowNewFolderButton = true;
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                return new PathParameter { IsOK = true, SelectPath = dialog.SelectedPath };
            }
            else
            {
                return new PathParameter { IsOK = false };
            }
        }

        /// <summary>
        /// 显示提问对话框
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        public bool ShowQuestion(string msg, string title = "请问")
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
        }
        #endregion


        #region 状态消息管理
        private StringBuilder statusMessage = new StringBuilder();

        public string InsertStatus(string msg)
        {
            statusMessage.Insert(0, $"{msg}\r\n");
            return StatusMessage;
        }

        public string AppendStatus(string msg)
        {
            statusMessage.AppendLine(msg);
            return StatusMessage;
        }

        public string StatusMessage
        {
            get { return statusMessage.ToString(); }
        }

        public void ClearStatus()
        {
            statusMessage.Clear();
        }

        #endregion

    }
}

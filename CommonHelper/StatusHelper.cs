using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    /// <summary>
    /// 状态消息管理
    /// </summary>
    public class StatusHelper
    {
        private StringBuilder statusMessage;
        public StatusHelper()
        {
            statusMessage = new StringBuilder();
        }

        /// <summary>
        /// 插入消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string InsertStatus(string msg)
        {
            statusMessage.Insert(0, $"{msg}\r\n");
            return StatusMessage;
        }

        /// <summary>
        /// 追加消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string AppendStatus(string msg)
        {
            statusMessage.AppendLine(msg);
            return StatusMessage;
        }

        /// <summary>
        /// 获取当前消息状态
        /// </summary>
        public string StatusMessage
        {
            get { return statusMessage.ToString(); }
        }

        /// <summary>
        /// 清空消息
        /// </summary>
        public void ClearStatus()
        {
            statusMessage.Clear();
        }
    }
}

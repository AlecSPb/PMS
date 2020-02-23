using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace XSHelper.Helpers
{
    /// <summary>
    /// 简单日志辅助类
    /// </summary>
    public class SimpleLogHelper
    {
        private string logFileName, currentUser;
        public SimpleLogHelper(string filename = "log.txt", string user = "None")
        {
            logFileName = filename;
            currentUser = user;
        }

        private void Log(string type, string msg)
        {
            if (File.Exists(logFileName))
            {
                File.AppendText($"[{DateTime.Now.ToString()}][{type}][{currentUser}]-[{msg}]");
            }
        }

        public void Info(string msg)
        {
            Log("Info", msg);
        }

        public void Warning(string msg)
        {
            Log("Warning", msg);
        }

        public void Debug(string msg)
        {
            Log("Debug", msg);
        }

        public void Fatal(string msg)
        {
            Log("Fatal", msg);
        }

        public void Error(string msg)
        {
            Log("Error", msg);
        }
    }
}

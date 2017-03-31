using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient;
using System.IO;

namespace PMSClient.Helpers
{
    public class LocalLog : ILog
    {
        private LogInformation _currentUser;
        private string _logfile;
        private string _errorfile;
        public LocalLog()
        {
            var currentdir = Environment.CurrentDirectory;
            var _logdir = Path.Combine(currentdir, "Log");
            var _errordir = Path.Combine(currentdir, "Error");
            if (!Directory.Exists(_logdir))
            {
                Directory.CreateDirectory(_logdir);
            }
            if (!Directory.Exists(_errordir))
            {
                Directory.CreateDirectory(_errordir);
            }
            //每天一个日志文件
            _logfile = Path.Combine(_logdir, $"mainlog{DateTime.Now.ToString("yyMMdd")}.txt");
            _errorfile = Path.Combine(_errordir, $"mainerror{ DateTime.Now.ToString("yyMMdd")}.txt");

            _currentUser = PMSHelper.CurrentLogInformation;
        }
        public void Log(string message)
        {
            var date = DateTime.Now;
            var user = _currentUser.CurrentUser.UserName;
            if (!File.Exists(_logfile))
            {
                File.Create(_logfile);
            }
            StreamWriter sw = new StreamWriter(_logfile);
            sw.WriteLine($"{user}:{date.ToString()}:{message}");
            sw.Close();
        }

        public void Error(string error)
        {
            var date = DateTime.Now;
            var user = _currentUser.CurrentUser.UserName;
            if (!File.Exists(_errorfile))
            {
                File.Create(_errorfile);
            }
            StreamWriter sw = new StreamWriter(_errorfile);
            sw.WriteLine($"{user}:{date.ToString()}:{error}");
            sw.Close();
        }

    }
}

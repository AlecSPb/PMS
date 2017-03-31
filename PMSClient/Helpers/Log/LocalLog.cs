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
        public LocalLog()
        {
            var currentdir= Environment.CurrentDirectory;
            var _logdir = Path.Combine(currentdir, "Log");
            if (!Directory.Exists(_logdir))
            {
                Directory.CreateDirectory(_logdir);
            }
            _logfile = Path.Combine(_logdir, "mainlog.txt");
            _currentUser = PMSUserHelper.CurrentLogInformation;
        }
        public void LogIt(string message)
        {

        }
    }
}

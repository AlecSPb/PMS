﻿using System;
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
            var currentdir = Environment.CurrentDirectory;
            var _logdir = Path.Combine(currentdir, "Log");
            if (!Directory.Exists(_logdir))
            {
                Directory.CreateDirectory(_logdir);
            }
            //每天一个日志文件
            _logfile = Path.Combine(_logdir, $"mainlog{DateTime.Now.ToString("yyMMdd")}.txt");
            _currentUser = PMSHelper.CurrentLogInformation;
        }
        public void LogIt(string message)
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
    }
}

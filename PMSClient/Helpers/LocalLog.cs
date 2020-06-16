using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient;
using System.IO;

namespace PMSClient.Helper
{
    public class LocalLog : ILog
    {
        private LogInformation _currentSession;
        private string _logfile;
        private string _errorfile;
        public LocalLog(LogInformation session)
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

            _currentSession = session;
        }
        public void Log(string message)
        {
            var date = DateTime.Now;
            var user = "none";
            if (_currentSession.CurrentUser != null)
            {
                user = _currentSession.CurrentUser.UserName;
            }
            if (!File.Exists(_logfile))
            {
                var fs = File.Create(_logfile);
                fs.Close();
                fs.Dispose();
            }
            var sw = new StreamWriter(_logfile, true);
            try
            {
                sw.WriteLine($"{user}+{date.ToString()}+{message}");
            }
            catch (Exception exx)
            {
                XSHelper.XS.MessageBox.ShowError(exx.Message);
            }
            finally
            {
                sw.Close();
            }

        }

        public void Error(Exception ex)
        {
            if (Properties.Settings.Default.ErrorDebugMsg)
            {
                XSHelper.XS.MessageBox.ShowError(ex.Message);
            }

            var date = DateTime.Now;
            var user = "none";
            if (_currentSession.CurrentUser != null)
            {
                user = _currentSession.CurrentUser.UserName;
            }

            if (!File.Exists(_errorfile))
            {
                var fs = File.Create(_errorfile);
                fs.Close();
                fs.Dispose();
            }

            var error = "未知错误发生";
            if (ex != null)
            {
                error = ex.Message;
            }
            var sw = new StreamWriter(_errorfile, true);
            try
            {
                sw.WriteLine($"{user}+{date.ToString()}+{error}");

            }
            catch (Exception exx)
            {
                XSHelper.XS.MessageBox.ShowError(exx.Message);
            }
            finally
            {
                sw.Close();
            }
        }

        public void Error(Exception ex, string position)
        {
            if (Properties.Settings.Default.ErrorDebugMsg)
            {
                XSHelper.XS.MessageBox.ShowError(ex.Message);
            }

            var date = DateTime.Now;
            var user = "none";
            if (_currentSession.CurrentUser != null)
            {
                user = _currentSession.CurrentUser.UserName;
            }

            if (!File.Exists(_errorfile))
            {
                var fs = File.Create(_errorfile);
                fs.Close();
                fs.Dispose();
            }

            var error = "未知错误发生";
            if (ex != null)
            {
                error = ex.Message;
            }
            var sw = new StreamWriter(_errorfile, true);
            try
            {
                sw.WriteLine($"[{user}]+[{position}]+[{date.ToString()}]+{error}");
            }
            catch (Exception exx)
            {
                XSHelper.XS.MessageBox.ShowError(exx.Message);
            }
            finally
            {
                sw.Close();
            }
        }
    }
}

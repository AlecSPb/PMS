using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PMSClient.UserService;
using PMSClient.Helpers;

namespace PMSClient
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            this.Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            CurrentUser = new UserService.DcUser()
            {
                ID = Guid.NewGuid(),
                UserName = "xs.zhou",
                RealName = "周新生",
                CreateTime = DateTime.Now,
                Email = "xs.zhou@outlook.com",
                Phone = "13540781789"
            };
            _logInformation = new LogInformation();
            _log = new LocalLog();
        }

        public DcUser CurrentUser { get; set; }
        private LogInformation _logInformation;
        public LogInformation CurrentLogInformation
        {
            get { return _logInformation; }
        }

        private ILog _log;
        public ILog CurrentLog
        {
            get { return _log; }
        }
    }
}

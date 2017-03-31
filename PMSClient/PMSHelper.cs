using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.UserService;
using PMSClient.Helper;

namespace PMSClient
{
    public static class PMSHelper
    {

        private static App _currentApp;
        static PMSHelper()
        {
            _currentApp = App.Current as App;
        }
        /// <summary>
        ///MainDesktop版本
        /// </summary>
        public static LogInformation CurrentLogInformation
        {
            get
            {
                return _currentApp.CurrentLogInformation;
            }
        }

        public static ILog CurrentLog
        {
            get
            {
                return _currentApp.CurrentLog;
            }
        }
    }
}

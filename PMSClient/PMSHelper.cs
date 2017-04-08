using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.UserService;
using PMSClient.Helper;
using PMSClient.ViewForDesktop;
using PMSClient.ViewForTablet;
using PMSClient.ViewModel;

namespace PMSClient
{
    public static class PMSHelper
    {

        static PMSHelper()
        {
            _currentSession = new LogInformation();
            _currentLog = new LocalLog();

            _desktopViews = new DesktopViewLocator();
            _tabletViews = new TabletViewLocator();
            _viewModels = (App.Current as App).FindResource("Locator") as ViewModelLocator;
        }
        public static LogInformation CurrentSession
        {
            get { return _currentSession; }
        }
        public static ILog CurrentLog
        {
            get { return _currentLog; }
        }

        #region 当前用户
        private static LogInformation _currentSession;
        private static ILog _currentLog;
        #endregion
        #region 日志组件

        #endregion

        #region 视图和视图模型
        private static DesktopViewLocator _desktopViews;
        private static TabletViewLocator _tabletViews;

        private static ViewModelLocator _viewModels;

        public static DesktopViewLocator DesktopViews
        {
            get { return _desktopViews; }
        }
        public static TabletViewLocator TabletViews
        {
            get { return _tabletViews; }
        }

        public static ViewModelLocator ViewModels
        {
            get { return _viewModels; }
        }
        #endregion
    }
}

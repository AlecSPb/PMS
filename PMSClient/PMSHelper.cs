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

        private static App _currentApp;
        static PMSHelper()
        {
            _currentApp = App.Current as App;

            _desktopViews = new DesktopViewLocator();
            _tabletViews = new TabletViewLocator();
            _viewModels = (App.Current as App).FindResource("Locator") as ViewModelLocator;
        }
        public static LogInformation CurrentSession
        {
            get
            {
                return _currentApp.CurrentSession;
            }
        }
        public static ILog CurrentLog
        {
            get
            {
                return _currentApp.CurrentLog;
            }
        }

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

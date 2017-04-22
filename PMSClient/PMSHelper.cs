using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.UserService;
using GalaSoft.MvvmLight.Views;
using PMSClient.Helper;
using PMSClient.View;
using PMSClient.ViewModel;
using PMSClient.Tool;

namespace PMSClient
{
    public static class PMSHelper
    {

        static PMSHelper()
        {
            _currentSession = new LogInformation();
            _currentLog = new LocalLog(_currentSession);

            _desktopViews = new DesktopViewLocator();
            _viewModels = (App.Current as App).FindResource("Locator") as ViewModelLocator;

            _toolViews = new ToolViewLocator();
            _toolViewModels = (App.Current as App).FindResource("ToolLocator") as ToolViewModelLocator;
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

        private static ViewModelLocator _viewModels;


        private static ToolViewLocator _toolViews;
        private static ToolViewModelLocator _toolViewModels;

        public static DesktopViewLocator DesktopViews
        {
            get { return _desktopViews; }
        }


        public static ViewModelLocator ViewModels
        {
            get { return _viewModels; }
        }


        public static ToolViewLocator ToolViews
        {
            get { return _toolViews; }
        }
        public static ToolViewModelLocator ToolViewModels
        {
            get
            {
                return _toolViewModels;
            }
        }
        #endregion
    }
}

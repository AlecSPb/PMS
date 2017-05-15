using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.Helper;

namespace PMSClient.ViewModel
{
    public class NavigationVM : ViewModelBase
    {
        private LogInformation _session;
        public NavigationVM()
        {
            _session = PMSHelper.CurrentSession;
            InitialNavigations();

            LogIn = new RelayCommand(ActionLogIn);
            LogOut = new RelayCommand(ActionLogOut);

            currentUserInformation = "暂无登录信息";
        }
        public void SetLogInformation(string logInformation)
        {
            CurrentUserInformation = logInformation;
        }

        #region 登录信息
        private string currentUserInformation;
        public string CurrentUserInformation
        {
            get { return currentUserInformation; }
            set { currentUserInformation = value; RaisePropertyChanged(nameof(CurrentUserInformation)); }
        }
        public RelayCommand LogIn { get; set; }
        public RelayCommand LogOut { get; set; }

        private void ActionLogOut()
        {
            PMSHelper.MainWindow.LogOut();
        }

        private void ActionLogIn()
        {
            NavigationService.GoTo(PMSViews.LogIn);
        }
        #endregion
        #region 导航信息
        private void InitialNavigations()
        {
            Notice = new RelayCommand(ActionNotice);

            GoToNavigation = new RelayCommand(() => NavigationService.GoTo(PMSViews.Navigation));
            GoToMaterialOrder = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialOrder), () => _session.IsAuthorized(PMSAccess.ReadMaterialOrder));
            GoToMaterialInventory = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialInventoryIn), () => _session.IsAuthorized(PMSAccess.ReadMaterialInventoryIn));

            GoToOutput = new RelayCommand(() => NavigationService.GoTo(PMSViews.Output), () => _session.IsAuthorized(PMSAccess.CanOutput));

            GoToDebug = new RelayCommand(() => NavigationService.GoTo(PMSViews.Debug), () => _session.IsAuthorized(PMSAccess.CanDebug));
        }

        private void ActionNotice()
        {
            PMSNotice.ShowNoticeWindow();
        }

        public RelayCommand GoToNavigation { get; set; }
        public RelayCommand GoToMaterialOrder { get; private set; }
        public RelayCommand GoToMaterialInventory { get; private set; }

        public RelayCommand GoToFeedBack { get; set; }
        public RelayCommand GoToOutput { get; set; }
        #endregion

        public RelayCommand Notice { get; set; }

        public RelayCommand GoToDebug { get; set; }
    }
}

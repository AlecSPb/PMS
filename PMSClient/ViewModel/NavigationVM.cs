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

            SetLogButtonVisbility(true);
        }
        #region 登录信息
        public void SetLogButtonVisbility(bool showlogin)
        {
            ShowLogInButton = showlogin;
            ShowLogOutButton = !ShowLogInButton;
        }

        private string currentUserInformation;
        public string CurrentUserInformation
        {
            get { return currentUserInformation; }
            set { currentUserInformation = value; RaisePropertyChanged(nameof(CurrentUserInformation)); }
        }
        public RelayCommand LogIn { get; set; }
        public RelayCommand LogOut { get; set; }

        private bool showLogInButton;
        public bool ShowLogInButton
        {
            get { return showLogInButton; }
            set { showLogInButton = value; RaisePropertyChanged(nameof(ShowLogInButton)); }
        }
        private bool showLogOutButton;

        public bool ShowLogOutButton
        {
            get { return showLogOutButton; }
            set { showLogOutButton = value; RaisePropertyChanged(nameof(ShowLogOutButton)); }
        }
        private void ActionLogOut()
        {
            throw new NotImplementedException();
        }

        private void ActionLogIn()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 导航信息
        private void InitialNavigations()
        {

            GoToOrder = new RelayCommand(() => NavigationService.GoTo(PMSViews.Order), () => _session.IsAuthorized("浏览订单"));
            GoToOrderCheck = new RelayCommand(() => NavigationService.GoTo(PMSViews.OrderCheck), () => _session.IsAuthorized("浏览订单核验"));
            GoToOrderStatistic = new RelayCommand(() => NavigationService.GoTo(PMSViews.OrderStatistic), () => _session.IsAuthorized("浏览订单统计"));


            GoToMaterialNeed = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialNeed), () => _session.IsAuthorized("浏览原料需求"));
            GoToMaterialOrder = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialOrder), () => _session.IsAuthorized("浏览原料订单"));
            GoToMaterialInventory = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialInventoryIn), () => _session.IsAuthorized("浏览原料库存"));

            GoToMisson = new RelayCommand(() => NavigationService.GoTo(PMSViews.Misson), () => _session.IsAuthorized("浏览任务"));
            GoToPlan = new RelayCommand(() => NavigationService.GoTo(PMSViews.Plan), () => _session.IsAuthorized("浏览计划安排"));
            GoToPlanStatistic = new RelayCommand(() => NavigationService.GoTo(PMSViews.PlanStatistic), () => _session.IsAuthorized("浏览计划统计"));


            GoToRecordMilling = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordMilling), () => _session.IsAuthorized("浏览制粉记录"));
            GoToRecordVHP = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordVHP), () => _session.IsAuthorized("浏览热压记录"));
            GoToRecordDeMold = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordDeMold), () => _session.IsAuthorized("浏览取模记录"));
            GoToRecordMachine = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordMachine), () => _session.IsAuthorized("浏览加工记录"));
            GoToRecordTest = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordTest), () => _session.IsAuthorized("浏览测试记录"));
            GoToRecordBonding = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordBonding), () => _session.IsAuthorized("浏览绑定记录"));


            GoToProduct = new RelayCommand(() => NavigationService.GoTo(PMSViews.Product), () => _session.IsAuthorized("浏览成品记录"));
            GoToDelivery = new RelayCommand(() => NavigationService.GoTo(PMSViews.Delivery), () => _session.IsAuthorized("浏览发货记录"));

            GoToMaintenance = new RelayCommand(() => NavigationService.GoTo(PMSViews.Maintanence), () => _session.IsAuthorized("浏览维护信息"));
            GoToBDCustomer = new RelayCommand(() => NavigationService.GoTo(PMSViews.BDCustomer), () => _session.IsAuthorized("浏览客户信息"));
            GoToBDCompound = new RelayCommand(() => NavigationService.GoTo(PMSViews.BDCompound), () => _session.IsAuthorized("浏览化合物信息"));
            GoToBDVHPDevice = new RelayCommand(() => NavigationService.GoTo(PMSViews.BDVHPDevice), () => _session.IsAuthorized("浏览热压设备信息"));
            GoToBDMold = new RelayCommand(() => NavigationService.GoTo(PMSViews.BDMold), () => _session.IsAuthorized("浏览模具信息"));
            GoToBDDeliveryAddress = new RelayCommand(() => NavigationService.GoTo(PMSViews.BDDeliveryAddress), () => _session.IsAuthorized("浏览发货地址信息"));
            GoToBDSupplier = new RelayCommand(() => NavigationService.GoTo(PMSViews.BDSupplier), () => _session.IsAuthorized("浏览供应商信息"));

            GoToAdminUser = new RelayCommand(() => NavigationService.GoTo(PMSViews.AdminUser), () => _session.IsAuthorized("浏览用户信息"));
            GoToAdminRole = new RelayCommand(() => NavigationService.GoTo(PMSViews.AdminRole), () => _session.IsAuthorized("浏览角色信息"));
            GoToAdminAccess = new RelayCommand(() => NavigationService.GoTo(PMSViews.AdminAccess), () => _session.IsAuthorized("浏览权限信息"));
        }

        public RelayCommand GoToOrder { get; private set; }
        public RelayCommand GoToOrderCheck { get; private set; }
        public RelayCommand GoToOrderStatistic { get; private set; }


        public RelayCommand GoToMisson { get; private set; }
        public RelayCommand GoToPlan { get; private set; }
        public RelayCommand GoToPlanStatistic { get; private set; }


        public RelayCommand GoToMaterialNeed { get; private set; }
        public RelayCommand GoToMaterialOrder { get; private set; }
        public RelayCommand GoToMaterialInventory { get; private set; }

        public RelayCommand GoToRecordMilling { get; private set; }
        public RelayCommand GoToRecordVHP { get; private set; }
        public RelayCommand GoToRecordDeMold { get; private set; }
        public RelayCommand GoToRecordMachine { get; private set; }
        public RelayCommand GoToRecordTest { get; private set; }
        public RelayCommand GoToRecordBonding { get; private set; }


        public RelayCommand GoToProduct { get; private set; }
        public RelayCommand GoToDelivery { get; private set; }


        public RelayCommand GoToMaintenance { get; set; }
        public RelayCommand GoToBDCustomer { get; set; }
        public RelayCommand GoToBDCompound { get; set; }
        public RelayCommand GoToBDVHPDevice { get; set; }
        public RelayCommand GoToBDMold { get; set; }
        public RelayCommand GoToBDDeliveryAddress { get; set; }
        public RelayCommand GoToBDSupplier { get; set; }


        public RelayCommand GoToAdminUser { get; set; }
        public RelayCommand GoToAdminRole { get; set; }
        public RelayCommand GoToAdminAccess { get; set; }
        #endregion
    }
}

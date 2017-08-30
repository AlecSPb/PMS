using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.Helper;
using System.IO;
using PMSClient.CustomControls;

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
            Notice = new RelayCommand(ActionNotice, CanNotice);
            Help = new RelayCommand(ActionHelp);


            GoToNavigation = new RelayCommand(() => NavigationService.GoTo(PMSViews.Navigation));
            GoToNavigationWorkFlow = new RelayCommand(() => NavigationService.GoTo(PMSViews.NavigationWorkFlow));

            GoToOrder = new RelayCommand(() => NavigationService.GoTo(PMSViews.Order), () => _session.IsAuthorized(PMSAccess.ReadOrder));
            GoToOrderCheck = new RelayCommand(() => NavigationService.GoTo(PMSViews.OrderCheck), () => _session.IsAuthorized(PMSAccess.ReadOrderCheck));
            GoToOutSource = new RelayCommand(() => NavigationService.GoTo(PMSViews.OutSource), () => _session.IsAuthorized(PMSAccess.ReadOutSource));

            GoToMaterialNeed = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialNeed), () => _session.IsAuthorized(PMSAccess.ReadMaterialNeed));
            GoToMaterialOrder = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialOrder), () => _session.IsAuthorized(PMSAccess.ReadMaterialOrder));
            GoToMaterialInventory = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialInventoryIn), () => _session.IsAuthorized(PMSAccess.ReadMaterialInventoryIn));
            GoToMaterialInventoryOut = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialInventoryOut), () => _session.IsAuthorized(PMSAccess.ReadMaterialInventoryOut));

            GoToPlate = new RelayCommand(() => NavigationService.GoTo(PMSViews.Plate), () => _session.IsAuthorized(PMSAccess.ReadPlate));

            GoToMisson = new RelayCommand(() => NavigationService.GoTo(PMSViews.Misson), () => _session.IsAuthorized(PMSAccess.ReadMisson));
            GoToPlan = new RelayCommand(() => NavigationService.GoTo(PMSViews.Plan), () => _session.IsAuthorized(PMSAccess.ReadPlan));
            GoToPlanConclusion = new RelayCommand(() => NavigationService.GoTo(PMSViews.PlanConclusion), () => _session.IsAuthorized(PMSAccess.ReadPlan));


            GoToRecordMilling = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordMilling), () => _session.IsAuthorized(PMSAccess.ReadRecordMilling));
            GoToRecordVHP = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordVHP), () => _session.IsAuthorized(PMSAccess.ReadRecordVHP));
            GoToRecordDeMold = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordDeMold), () => _session.IsAuthorized(PMSAccess.ReadRecordDeMold));
            GoToRecordMachine = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordMachine), () => _session.IsAuthorized(PMSAccess.ReadRecordMachine));
            GoToRecordTest = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordTest), () => _session.IsAuthorized(PMSAccess.ReadRecordTest));
            GoToRecordBonding = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordBonding), () => _session.IsAuthorized(PMSAccess.ReadRecordBonding));


            GoToProduct = new RelayCommand(() => NavigationService.GoTo(PMSViews.Product), () => _session.IsAuthorized(PMSAccess.ReadProduct));
            GoToDelivery = new RelayCommand(() => NavigationService.GoTo(PMSViews.Delivery), () => _session.IsAuthorized(PMSAccess.ReadDelivery));
            GoToDeliveryItemList = new RelayCommand(() => NavigationService.GoTo(PMSViews.DeliveryItemList), () => _session.IsAuthorized(PMSAccess.ReadDelivery));

            GoToMaintenance = new RelayCommand(() => NavigationService.GoTo(PMSViews.Maintanence), () => _session.IsAuthorized(PMSAccess.ReadMaintenance));
            GoToBDCustomer = new RelayCommand(() => NavigationService.GoTo(PMSViews.Customer), () => _session.IsAuthorized(PMSAccess.ReadCustomer));
            #region 临时
            //GoToBDCompound = new RelayCommand(() => NavigationService.GoTo(PMSViews.BDCompound), () => _session.IsAuthorized("浏览化合物信息"));
            //GoToBDVHPDevice = new RelayCommand(() => NavigationService.GoTo(PMSViews.BDVHPDevice), () => _session.IsAuthorized("浏览热压设备信息"));
            //GoToBDMold = new RelayCommand(() => NavigationService.GoTo(PMSViews.BDMold), () => _session.IsAuthorized("浏览模具信息"));
            //GoToBDDeliveryAddress = new RelayCommand(() => NavigationService.GoTo(PMSViews.BDDeliveryAddress), () => _session.IsAuthorized("浏览发货地址信息"));
            //GoToBDSupplier = new RelayCommand(() => NavigationService.GoTo(PMSViews.BDSupplier), () => _session.IsAuthorized("浏览供应商信息"));

            //GoToAdminUser = new RelayCommand(() => NavigationService.GoTo(PMSViews.AdminUser), () => _session.IsAuthorized("浏览用户信息"));
            //GoToAdminRole = new RelayCommand(() => NavigationService.GoTo(PMSViews.AdminRole), () => _session.IsAuthorized("浏览角色信息"));
            //GoToAdminAccess = new RelayCommand(() => NavigationService.GoTo(PMSViews.AdminAccess), () => _session.IsAuthorized("浏览权限信息"));
            #endregion
            GoToStatisticOrder = new RelayCommand(() => NavigationService.GoTo(PMSViews.StatisticOrder), () => _session.IsAuthorized(PMSAccess.ReadStatisticOrder));
            GoToStatisticPlan = new RelayCommand(() => NavigationService.GoTo(PMSViews.StatisticPlan), () => _session.IsAuthorized(PMSAccess.ReadStatisticPlan));
            GoToStatisticDelivery = new RelayCommand(() => NavigationService.GoTo(PMSViews.StatisticDelivery), () => _session.IsAuthorized(PMSAccess.ReadStatisticDelivery));
            GoToStatisticProduct = new RelayCommand(() => NavigationService.GoTo(PMSViews.StatisticProduct), () => _session.IsAuthorized(PMSAccess.ReadStatisticProduct));

            GoToFeedBack = new RelayCommand(() => NavigationService.GoTo(PMSViews.FeedBack), () => _session.IsAuthorized(PMSAccess.ReadFeedback));
            GoToOutput = new RelayCommand(() => NavigationService.GoTo(PMSViews.Output), () => _session.IsAuthorized(PMSAccess.CanOutput));

            GoToDebug = new RelayCommand(() => NavigationService.GoTo(PMSViews.Debug), () => _session.IsAuthorized(PMSAccess.CanDebug));

            CodeRule = new RelayCommand(ActionCodeRule);

            GoToHistory = new RelayCommand(() => NavigationService.GoTo(PMSViews.History), () => _session.IsAuthorized(PMSAccess.CanHistory));
        }

        private void ActionCodeRule()
        {
            try
            {
                string helpFileName = "CodeRule.docx";
                string helpFile = System.IO.Path.Combine(Environment.CurrentDirectory, "Resource", "Files", helpFileName);
                if (File.Exists(helpFile))
                {
                    System.Diagnostics.Process.Start(helpFile);
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private bool CanNotice()
        {
            return PMSNotice.HasNewNotice;
        }

        private void ActionHelp()
        {
            try
            {
                string helpFileName = "";
                if (PMSHelper.Language == "zh-cn")
                {
                    helpFileName = "pmshelp_ch.pptx";
                }
                else
                {
                    helpFileName = "pmshelp_en.pptx";
                }

                string helpFile = System.IO.Path.Combine(Environment.CurrentDirectory, "Resource", "Files", helpFileName);
                if (File.Exists(helpFile))
                {
                    System.Diagnostics.Process.Start(helpFile);
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void ActionNotice()
        {
            if (PMSNotice.HasNewNotice)
            {
                NoticeWindow win = new NoticeWindow();
                win.NoticeData = PMSNotice.Notices;
                if (win.ShowDialog() == true)
                {
                    PMSNotice.SaveCurrentCount();
                }
            }
            else
            {
                PMSDialogService.ShowYes("暂无消息");
            }
        }

        public RelayCommand GoToNavigation { get; set; }
        public RelayCommand GoToNavigationWorkFlow { get; set; }
        public RelayCommand GoToOrder { get; private set; }
        public RelayCommand GoToOrderCheck { get; private set; }
        public RelayCommand GoToOutSource { get; private set; }

        public RelayCommand GoToMisson { get; private set; }
        public RelayCommand GoToPlan { get; private set; }
        public RelayCommand GoToPlanConclusion { get; private set; }

        public RelayCommand GoToPlate { get; private set; }

        public RelayCommand GoToMaterialNeed { get; private set; }
        public RelayCommand GoToMaterialOrder { get; private set; }
        public RelayCommand GoToMaterialInventory { get; private set; }
        public RelayCommand GoToMaterialInventoryOut { get; private set; }
        public RelayCommand GoToRecordMilling { get; private set; }
        public RelayCommand GoToRecordVHP { get; private set; }
        public RelayCommand GoToRecordDeMold { get; private set; }
        public RelayCommand GoToRecordMachine { get; private set; }
        public RelayCommand GoToRecordTest { get; private set; }
        public RelayCommand GoToRecordBonding { get; private set; }


        public RelayCommand GoToProduct { get; private set; }
        public RelayCommand GoToDelivery { get; private set; }
        public RelayCommand GoToDeliveryItemList { get; private set; }

        public RelayCommand GoToStatisticOrder { get; private set; }
        public RelayCommand GoToStatisticDelivery { get; private set; }
        public RelayCommand GoToStatisticPlan { get; private set; }
        public RelayCommand GoToStatisticProduct { get; private set; }

        public RelayCommand GoToMaintenance { get; set; }
        public RelayCommand GoToBDCustomer { get; set; }
        public RelayCommand GoToBDCompound { get; set; }
        public RelayCommand GoToBDVHPDevice { get; set; }
        public RelayCommand GoToBDMold { get; set; }
        public RelayCommand GoToBDDeliveryAddress { get; set; }
        public RelayCommand GoToBDSupplier { get; set; }

        public RelayCommand GoToFeedBack { get; set; }
        public RelayCommand GoToOutput { get; set; }
        public RelayCommand GoToHistory { get; set; }
        public RelayCommand CodeRule { get; set; }

        //public RelayCommand GoToAdminUser { get; set; }
        //public RelayCommand GoToAdminRole { get; set; }
        //public RelayCommand GoToAdminAccess { get; set; }
        #endregion

        public RelayCommand Notice { get; set; }
        public RelayCommand Help { get; set; }
        public RelayCommand GoToDebug { get; set; }
    }
}

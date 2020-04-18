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
using PMSClient.ToolWindow;
using PMSClient.MainService;

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

            OrderCount = PlanedCount = 0;
            InitializeData();
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
            #region 命令初始化
            Notice = new RelayCommand(ActionNotice, CanNotice);
            Help = new RelayCommand(ActionHelp);


            GoToNavigation = new RelayCommand(() => NavigationService.GoTo(PMSViews.Navigation));
            GoToNavigationWorkFlow = new RelayCommand(() => NavigationService.GoTo(PMSViews.NavigationWorkFlow));

            GoToOrder = new RelayCommand(() => NavigationService.GoTo(PMSViews.Order), () => _session.IsAuthorized(PMSAccess.ReadOrder)
            || _session.IsInGroup(AccessGrant.ViewAllModule));
            GoToOutSource = new RelayCommand(() => NavigationService.GoTo(PMSViews.OutSource), () => _session.IsAuthorized(PMSAccess.ReadOutSource)
            || _session.IsInGroup(AccessGrant.ViewAllModule));

            GoToMaterialNeed = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialNeed), () => _session.IsAuthorized(PMSAccess.ReadMaterialNeed)
            || _session.IsInGroup(AccessGrant.ViewAllModule));
            GoToMaterialOrder = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialOrder), () => _session.IsAuthorized(PMSAccess.ReadMaterialOrder)
            || _session.IsInGroup(AccessGrant.ViewAllModule));
            GoToMaterialOrderItemList = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialOrderItemList), () => _session.IsAuthorized(PMSAccess.ReadMaterialOrder)
            || _session.IsInGroup(AccessGrant.ViewAllModule));
            GoToMaterialInventory = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialInventoryIn), () => _session.IsAuthorized(PMSAccess.ReadMaterialInventoryIn)
            || _session.IsInGroup(AccessGrant.ViewAllModule));
            GoToMaterialInventoryOut = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialInventoryOut), () => _session.IsAuthorized(PMSAccess.ReadMaterialInventoryOut)
            || _session.IsInGroup(AccessGrant.ViewAllModule));

            GoToPlate = new RelayCommand(() => NavigationService.GoTo(PMSViews.Plate), () => _session.IsAuthorized(PMSAccess.ReadProduct)
            || _session.IsInGroup(AccessGrant.ViewAllModule));

            GoToMisson = new RelayCommand(() => NavigationService.GoTo(PMSViews.Misson), () => _session.IsAuthorized(PMSAccess.ReadMisson)
            || _session.IsInGroup(AccessGrant.ViewAllModule));
            GoToPlan = new RelayCommand(() => NavigationService.GoTo(PMSViews.Plan), () => _session.IsAuthorized(PMSAccess.ReadPlan)
            || _session.IsInGroup(AccessGrant.ViewAllModule));
            GoToPlanConclusion = new RelayCommand(() => NavigationService.GoTo(PMSViews.PlanConclusion), () => _session.IsAuthorized(PMSAccess.ReadPlan)
            || _session.IsInGroup(AccessGrant.ViewAllModule));


            GoToRecordMilling = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordMilling), () => _session.IsAuthorized(PMSAccess.ReadRecordMilling)
            || _session.IsInGroup(AccessGrant.ViewAllModule));
            GoToRecordVHP = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordVHP), () => _session.IsAuthorized(PMSAccess.ReadRecordVHP)
            || _session.IsInGroup(AccessGrant.ViewAllModule));
            GoToRecordDeMold = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordDeMold), () => _session.IsAuthorized(PMSAccess.ReadRecordDeMold)
            || _session.IsInGroup(AccessGrant.ViewAllModule));
            GoToRecordMachine = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordMachine), () => _session.IsAuthorized(PMSAccess.ReadRecordMachine)
            || _session.IsInGroup(AccessGrant.ViewAllModule));
            GoToRecordTest = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordTest), () => _session.IsAuthorized(PMSAccess.ReadRecordTest)
            || _session.IsInGroup(AccessGrant.ViewAllModule));
            GoToRecordBonding = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordBonding), () => _session.IsAuthorized(PMSAccess.ReadRecordBonding)
            || _session.IsInGroup(AccessGrant.ViewAllModule));


            GoToProduct = new RelayCommand(() => NavigationService.GoTo(PMSViews.Product), () => _session.IsAuthorized(PMSAccess.ReadProduct)
            || _session.IsInGroup(AccessGrant.ViewAllModule));
            GoToDelivery = new RelayCommand(() => NavigationService.GoTo(PMSViews.Delivery),
                () => _session.IsInGroup(AccessGrant.ViewDeliveryItemList)
                || _session.IsInGroup(AccessGrant.ViewAllModule));
            GoToDeliveryItemList = new RelayCommand(() => NavigationService.GoTo(PMSViews.DeliveryItemList),
                () => _session.IsInGroup(AccessGrant.ViewDeliveryItemList)
                || _session.IsInGroup(AccessGrant.ViewAllModule));

            GoToBDCustomer = new RelayCommand(() => NavigationService.GoTo(PMSViews.Customer), () => _session.IsAuthorized(PMSAccess.ReadCustomer)
            || _session.IsInGroup(AccessGrant.ViewAllModule));
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
            GoToStatisticOrder = new RelayCommand(() => NavigationService.GoTo(PMSViews.StatisticOrder), () => _session.IsAuthorized(PMSAccess.ReadStatisticOrder)
            || _session.IsInGroup(AccessGrant.ViewAllModule));
            GoToStatisticPlan = new RelayCommand(() => NavigationService.GoTo(PMSViews.StatisticPlan), () => _session.IsAuthorized(PMSAccess.ReadStatisticPlan)
            || _session.IsInGroup(AccessGrant.ViewAllModule));
            GoToStatisticDelivery = new RelayCommand(() => NavigationService.GoTo(PMSViews.StatisticDelivery), () => _session.IsAuthorized(PMSAccess.ReadStatisticDelivery)
            || _session.IsInGroup(AccessGrant.ViewAllModule));
            GoToStatisticProduct = new RelayCommand(() => NavigationService.GoTo(PMSViews.StatisticProduct), () => _session.IsAuthorized(PMSAccess.ReadStatisticProduct)
            || _session.IsInGroup(AccessGrant.ViewAllModule));

            GoToFeedBack = new RelayCommand(() => NavigationService.GoTo(PMSViews.FeedBack), () => _session.IsAuthorized(PMSAccess.ReadFeedback)
            || _session.IsInGroup(AccessGrant.ViewAllModule));
            GoToTool = new RelayCommand(() => NavigationService.GoTo(PMSViews.Tool));

            GoToDebug = new RelayCommand(() => NavigationService.GoTo(PMSViews.Debug), () => _session.IsAuthorized(PMSAccess.CanDebug)
            || _session.IsInGroup(AccessGrant.ViewAllModule));

            CodeRule = new RelayCommand(ActionCodeRule);

            GoToHistory = new RelayCommand(() => NavigationService.GoTo(PMSViews.History), () => _session.IsAuthorized(PMSAccess.CanHistory)
            || _session.IsInGroup(AccessGrant.ViewAllModule));

            GoToBDCompound = new RelayCommand(() => NavigationService.GoTo(PMSViews.BDCompound));

            GoToIntegratedSearch = new RelayCommand(() =>
            {
                //ToolWindow.ComplexQueryTool tool = new ToolWindow.ComplexQueryTool();
                //tool.Show();

                var tool = new ToolWindow.IntegratedSearch();
                tool.Show();
            }, () => _session.IsInGroup(AccessGrant.ViewAllModule));

            ImportantCode = new RelayCommand(() =>
            {
                string fileContent = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Documents", "keycodeInformaton.txt"));
                PlainTextWindow win = new PlainTextWindow();
                win.Title = "重要编码";
                win.ContentText = fileContent;
                win.ShowDialog();
            });

            LaserRule = new RelayCommand(ActionLaserRule);

            ToDoList = new RelayCommand(() => NavigationService.GoTo(PMSViews.ToDo));


            GoToFillingTool = new RelayCommand(() => NavigationService.GoTo(PMSViews.FillingTool));
            GoToMillingTool = new RelayCommand(() => NavigationService.GoTo(PMSViews.MillingTool));

            //数据导出
            GoToOutput = new RelayCommand(() =>
              {
                  var window = new ToolWindow.DataOutputWindow();
                  window.Show();
              });

            GoToFailure = new RelayCommand(() => NavigationService.GoTo(PMSViews.Failure));
            GoToPlanForProduct = new RelayCommand(() =>
              {
                  NavigationService.GoTo(PMSViews.PlanTrace);
              }, () => _session.IsInGroup(AccessGrant.ViewAllModule));

            //PMI计数模块
            GoToCounter = new RelayCommand(() =>
              {
                  NavigationService.GoTo(PMSViews.PMICounter);
              }, () => _session.IsInGroup(AccessGrant.ViewAllModule));

            //储备库存
            GoToRemainInventory = new RelayCommand(() =>
              {
                  NavigationService.GoTo(PMSViews.RemainInventory);
              }, () => _session.IsInGroup(AccessGrant.ViewAllModule));

            GoToOutsideProcess = new RelayCommand(() =>
              {
                  NavigationService.GoTo(PMSViews.OutsideProcess);
              }, () => _session.IsInGroup(AccessGrant.ViewOutsideProcess));

            GoToSample = new RelayCommand(() =>
              {
                  NavigationService.GoTo(PMSViews.Sample);
              }, () => _session.IsInGroup(AccessGrant.ViewAllModule) || _session.IsInGroup(AccessGrant.Sample));

            GoToRawMaterialSheet = new RelayCommand(() =>
            {
                NavigationService.GoTo(PMSViews.RawMaterialSheet);
            }, () => _session.IsInGroup(AccessGrant.ViewAllModule));
            #endregion
        }

        private void ActionLaserRule()
        {
            try
            {
                string helpFileName = "LaserRule.docx";
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
                PMSDialogService.Show("暂无消息");
            }
        }

        public RelayCommand GoToNavigation { get; set; }
        public RelayCommand GoToNavigationWorkFlow { get; set; }
        public RelayCommand GoToOrder { get; private set; }
        public RelayCommand GoToOutSource { get; private set; }

        public RelayCommand GoToMisson { get; private set; }
        public RelayCommand GoToPlan { get; private set; }
        public RelayCommand GoToPlanConclusion { get; private set; }

        public RelayCommand GoToPlate { get; private set; }

        public RelayCommand GoToMaterialNeed { get; private set; }
        public RelayCommand GoToMaterialOrder { get; private set; }
        public RelayCommand GoToMaterialOrderItemList { get; set; }
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

        public RelayCommand GoToBDCustomer { get; set; }
        public RelayCommand GoToBDCompound { get; set; }
        public RelayCommand GoToBDVHPDevice { get; set; }
        public RelayCommand GoToBDMold { get; set; }
        public RelayCommand GoToBDDeliveryAddress { get; set; }
        public RelayCommand GoToBDSupplier { get; set; }

        public RelayCommand GoToFeedBack { get; set; }
        public RelayCommand GoToTool { get; set; }
        public RelayCommand GoToHistory { get; set; }
        public RelayCommand CodeRule { get; set; }

        public RelayCommand GoToIntegratedSearch { get; set; }


        public RelayCommand GoToOutput { get; set; }

        public RelayCommand GoToPlanForProduct { get; set; }

        //public RelayCommand GoToAdminUser { get; set; }
        //public RelayCommand GoToAdminRole { get; set; }
        //public RelayCommand GoToAdminAccess { get; set; }
        #endregion

        public RelayCommand Notice { get; set; }
        public RelayCommand Help { get; set; }
        public RelayCommand GoToDebug { get; set; }

        public RelayCommand ImportantCode { get; set; }
        public RelayCommand LaserRule { get; set; }

        public RelayCommand ToDoList { get; set; }

        public RelayCommand GoToFillingTool { get; set; }
        public RelayCommand GoToMillingTool { get; set; }

        public RelayCommand GoToFailure { get; set; }

        public RelayCommand GoToCounter { get; set; }

        public RelayCommand GoToRemainInventory { get; set; }
        public RelayCommand GoToOutsideProcess { get; set; }
        public RelayCommand GoToSample { get; set; }
        public RelayCommand GoToRawMaterialSheet { get; set; }

        private void InitializeData()
        {
            try
            {
                using (var service = new OrderServiceClient())
                {
                    OrderCount = service.GetOrderCount(string.Empty, string.Empty, string.Empty);
                    UnFinishedOrderCount = service.GetOrderCountUnCompleted(string.Empty, string.Empty);
                }
                using (var service = new PlanVHPServiceClient())
                {
                    PlanedCount = service.GetPlanCount();
                }
            }
            catch (Exception)
            {

            }
        }
        #region 属性
        private int orderCount;
        public int OrderCount
        {
            get
            {
                return orderCount;
            }
            set
            {
                if (orderCount == value)
                {
                    return;
                }
                orderCount = value;
                RaisePropertyChanged(nameof(OrderCount));
            }
        }
        private int planedCount;
        public int PlanedCount
        {
            get
            {
                return planedCount;
            }
            set
            {
                if (planedCount == value)
                {
                    return;
                }
                planedCount = value;
                RaisePropertyChanged(nameof(PlanedCount));
            }
        }

        private int unFinishedOrderCount;
        public int UnFinishedOrderCount
        {
            get
            {
                return unFinishedOrderCount;
            }
            set
            {
                if (unFinishedOrderCount == value)
                    return;
                unFinishedOrderCount = value;
                RaisePropertyChanged(nameof(UnFinishedOrderCount));
            }
        }
        #endregion
    }
}

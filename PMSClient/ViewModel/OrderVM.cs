using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.IO;
using PMSClient.ToolWindow;
using PMSClient.NewService;
using System.Windows.Media.Imaging;
using System.Text;
using PMSClient.Components.EOrder;
using XSHelper;
using Newtonsoft.Json;

namespace PMSClient.ViewModel
{
    public class OrderVM : BaseViewModelPage
    {
        public OrderVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        /// <summary>
        ///综合搜索
        /// </summary>
        /// <param name="pminumber"></param>
        public void SetSearch(string pminumber)
        {
            SearchCustomer = "";
            SearchCompositionStandard = "";
            SearchPMINumber = pminumber;
            SearchOrderState = "";
            SetPageParametersWhenConditionChange();
        }

        public List<string> SearchOrderStates { get; set; }

        /// <summary>
        /// 用于编辑后刷新订单调用
        /// </summary>
        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }


        private void InitializeProperties()
        {
            searchCustomer = "";
            searchCompositionStandard = "";
            searchPMINumber = "";
            searchOrderState = PMSCommon.OrderState.未完成.ToString();
            MainOrders = new ObservableCollection<DcOrder>();
            UnFinishedOrderCount = 0;
            UnFinishedTargetCount = 0;

            SearchOrderStates = new List<string>();
            SearchOrderStates.Add(PMSCommon.OrderState.未核验.ToString());
            SearchOrderStates.Add(PMSCommon.OrderState.未完成.ToString());
            SearchOrderStates.Add(PMSCommon.OrderState.生产完成.ToString());
            SearchOrderStates.Add(PMSCommon.OrderState.最终完成.ToString());
            SearchOrderStates.Add(PMSCommon.OrderState.暂停.ToString());
            SearchOrderStates.Add(PMSCommon.OrderState.取消.ToString());
            SearchOrderStates.Add("");
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);

            Add = new RelayCommand(ActionAdd, CanAdd);

            Edit = new RelayCommand<DcOrder>(ActionEdit, CanEdit);

            Duplicate = new RelayCommand<DcOrder>(ActionDuplicate, CanDuplicate);
            Check = new RelayCommand<DcOrder>(ActionCheck, CanCheck);
            Sample = new RelayCommand<DcOrder>(ActionSample, CanSample);

            SelectionChanged = new RelayCommand<DcOrder>(ActionSelectionChanged);
            Output = new RelayCommand(ActionOutput);

            SampleSheet = new RelayCommand(ActionSampleSheet);

            ShowDetails = new RelayCommand<string>(ActionShowDetails);
            SpecialSituation = new RelayCommand(ActionSpecialSituation);
            JsonCheck = new RelayCommand(ActionJsonCheck);
        }

        private void ActionJsonCheck()
        {
            try
            {
                var dialogResult = XS.Dialog.ShowFolderBrowserDialog("请选择json订单文件所在的文件夹");
                if (dialogResult.HasSelected)
                {
                    string folderpath = dialogResult.SelectPath;
                    if (Directory.Exists(folderpath))
                    {
                        StringBuilder sb = new StringBuilder();

                        List<Order> orders = new List<Order>();
                        foreach (var item in Directory.GetFiles(folderpath, "*.json"))
                        {
                            string jsonFile = item;
                            string jsonStr = XS.File.ReadText(jsonFile);
                            var order = JsonConvert.DeserializeObject<Order>(jsonStr);
                            orders.Add(order);
                        }
                        //产生窗口
                        var win = new TextWindow();
                        win.CurrentOrders = orders;
                        win.Show();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void ActionSpecialSituation()
        {
            var win = new ImageWindow();
            string imagePath = Path.Combine(Environment.CurrentDirectory, "HelpDocs", "specialsituation_order.jpg");
            BitmapImage imgSource = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            win.MainImage.Source = imgSource;
            win.ShowDialog();
        }

        private void ActionShowDetails(string parameter)
        {
            if (!string.IsNullOrEmpty(parameter))
            {
                string s = "";
                switch (parameter)
                {
                    case "SpecialRequirement":
                        s = CurrentOrder?.SpecialRequirement;
                        break;
                    default:
                        break;
                }
                if (s != "")
                {
                    var dialog = new ToolWindow.PlainTextWindow();
                    dialog.ContentText = s;
                    dialog.ShowDialog();
                }
            }
        }

        private void ActionEdit(DcOrder obj)
        {
            //修改提示
            if (!PMSDialogService.ShowYesNo("请问", "确定要【修改】这个订单吗？"))
                return;
            PMSHelper.ViewModels.OrderEdit.SetEdit(obj);
            NavigationService.GoTo(PMSViews.OrderEdit);
        }

        private bool CanSample(DcOrder arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditOrderCheck);
        }

        private void ActionSample(DcOrder obj)
        {
            VMHelper.OrderVMHelper.AddSampleFromOrder(obj);
        }
        private void ActionSampleSheet()
        {
            if (!PMSDialogService.ShowYesNo("请问", "即将输出 未完成订单中 包含样品需求的订单，继续？"))
                return;

            try
            {
                var report = new ReportsHelperNew.ReportSampleSheet();
                report.Intialize("样品需求");
                report.Output();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void ActionOutput()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定要导出订单数据吗？"))
            {
                return;
            }
            try
            {
                //年月选择对话框
                var dialog = new WPFControls.YearDateDailog(0);

                if (dialog.ShowDialog() == false)
                {
                    return;
                }

                int year_start = dialog.YearStart;
                int month_start = dialog.MonthStart;
                int year_end = dialog.YearEnd;
                int month_end = dialog.MonthEnd;


                var excel = new ExcelOutputHelper.ExcelOutputOrder();
                excel.Intialize("订单数据", "Data", 50);
                excel.SetParameter(year_start, month_start, year_end, month_end);
                excel.Output();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                PMSDialogService.Show(ex.Message);
            }

        }

        private void ActionSelectionChanged(DcOrder model)
        {
            if (model != null)
            {
                CurrentOrder = model;
            }
        }

        /// <summary>
        /// 核验订单权限编码=编辑订单核验
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool CanCheck(DcOrder arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditOrderCheck);
        }

        private void ActionCheck(DcOrder order)
        {
            if (order != null)
            {
                //PMSHelper.ViewModels.OrderCheckEdit.SetEdit(order);
                //NavigationService.GoTo(PMSViews.OrderCheckEdit);
                //使用窗口的方式进行核验
                try
                {
                    OrderCheckDialog dg = new OrderCheckDialog();
                    dg.SetCurrentOrder(order.ID);
                    dg.ShowDialog();
                    SetPageParametersWhenConditionChange();
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
        }
        #region 权限控制代码=编辑订单
        private bool CanDuplicate(DcOrder arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑订单");
        }
        private bool CanEdit(DcOrder arg)
        {
            bool isOK = true;
            if (arg != null)
            {
                if (arg.State != "未核验")
                {
                    isOK = false;
                }
            }
            return PMSHelper.CurrentSession.IsAuthorized("编辑订单") && isOK;
        }
        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑订单");
        }
        #endregion


        private void ActionDuplicate(DcOrder order)
        {
            if (PMSDialogService.ShowYesNo("请问", "确定复用这条记录？"))
            {
                if (order != null)
                {
                    PMSHelper.ViewModels.OrderEdit.SetDuplicate(order);
                    NavigationService.GoTo(PMSViews.OrderEdit);
                }
            }

        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.OrderEdit.SetNew();
            NavigationService.GoTo(PMSViews.OrderEdit);
        }

        private bool CanSearch()
        {
            return true;
        }

        private void ActionAll()
        {
            SearchCustomer = "";
            SearchCompositionStandard = "";
            SearchPMINumber = "";
            SearchOrderState = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            try
            {
                PageIndex = 1;
                PageSize = 30;
                using (var service = new NewServiceClient())
                {
                    RecordCount = service.GetOrderCount(SearchCustomer, SearchCompositionStandard, SearchPMINumber, SearchOrderState);
                    //获得未完成的订单数
                    UnFinishedOrderCount = service.GetOrderUnFinishedCount();

                    UnFinishedTargetCount = service.GetOrderUnFinishedTargetCount();
                    service.Close();
                }
                ActionPaging();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            try
            {
                int skip, take = 0;
                skip = (PageIndex - 1) * PageSize;
                take = PageSize;
                using (var service = new NewServiceClient())
                {
                    var orders = service.GetOrder(skip, take, SearchCustomer, SearchCompositionStandard, SearchPMINumber, SearchOrderState);
                    MainOrders.Clear();
                    orders.ToList().ForEach(o => MainOrders.Add(o));
                    CurrentOrder = MainOrders.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

        }


        #region Proeperties
        private int unFinishedOrderCount;
        public int UnFinishedOrderCount
        {
            get { return unFinishedOrderCount; }
            set
            {
                unFinishedOrderCount = value;
                RaisePropertyChanged(nameof(UnFinishedOrderCount));
            }
        }

        private int unFinishedTargetCount;
        public int UnFinishedTargetCount
        {
            get
            {
                return unFinishedTargetCount;
            }
            set
            {
                unFinishedTargetCount = value;
                RaisePropertyChanged(nameof(UnFinishedTargetCount));
            }
        }

        private string searchOrderState;
        public string SearchOrderState
        {
            get { return searchOrderState; }
            set
            {
                if (searchOrderState == value)
                    return;
                searchOrderState = value;
                RaisePropertyChanged(() => SearchOrderState);
            }
        }

        private string searchPMINumber;
        public string SearchPMINumber
        {
            get { return searchPMINumber; }
            set
            {
                if (searchPMINumber == value)
                    return;
                searchPMINumber = value;
                RaisePropertyChanged(() => SearchPMINumber);
            }
        }
        private string searchCustomer;
        public string SearchCustomer
        {
            get { return searchCustomer; }
            set
            {
                if (searchCustomer == value)
                    return;
                searchCustomer = value;
                RaisePropertyChanged(() => SearchCustomer);
            }
        }
        private string searchCompositionStandard;
        public string SearchCompositionStandard
        {
            get { return searchCompositionStandard; }
            set
            {
                if (searchCompositionStandard == value)
                    return;
                searchCompositionStandard = value;
                RaisePropertyChanged(() => SearchCompositionStandard);
            }
        }

        private DcOrder currentOrder;

        public DcOrder CurrentOrder
        {
            get { return currentOrder; }
            set { currentOrder = value; RaisePropertyChanged(nameof(CurrentOrder)); }
        }




        private ObservableCollection<DcOrder> mainOrders;
        public ObservableCollection<DcOrder> MainOrders
        {
            get { return mainOrders; }
            set { mainOrders = value; RaisePropertyChanged(nameof(MainOrders)); }
        }

        #endregion

        #region Commands
        public RelayCommand Add { get; private set; }
        public RelayCommand<DcOrder> Edit { get; private set; }
        public RelayCommand<DcOrder> Duplicate { get; private set; }
        public RelayCommand<DcOrder> Check { get; private set; }
        public RelayCommand<DcOrder> Sample { get; private set; }
        public RelayCommand<DcOrder> SelectionChanged { get; set; }
        public RelayCommand Output { get; set; }
        public RelayCommand SampleSheet { get; set; }
        public RelayCommand<string> ShowDetails { get; private set; }

        public RelayCommand SpecialSituation { get; set; }
        public RelayCommand JsonCheck { get; set; }

        #endregion
    }
}

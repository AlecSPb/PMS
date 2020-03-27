using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.IO;
using PMSClient.ToolWindow;
using PMSClient.NewService;


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
            searchOrderState = "";
            MainOrders = new ObservableCollection<DcOrder>();
            UnFinishedOrderCount = 0;
            UnFinishedTargetCount = 0;

            SearchOrderStates = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.OrderState>(SearchOrderStates);
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
            if (!PMSDialogService.ShowYesNo("询问", "数据导出时间会比较长，请在弹出完成对话框之前不要进行其他操作。\r\n确定明白请点确定开始"))
            {
                return;
            }
            PMSDialogService.UnImplementyet();
            PMSDialogService.Show("数据导出完成到桌面，请右键-打开方式-Excel打开文件");
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
        #endregion
    }
}

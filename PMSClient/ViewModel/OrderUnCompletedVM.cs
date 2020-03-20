using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using System.Windows;
using PMSClient.ToolWindow;
using PMSClient.Sample;

namespace PMSClient.ViewModel
{
    public class OrderUnCompletedVM : BaseViewModelPage
    {
        public OrderUnCompletedVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

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
            MainOrders = new ObservableCollection<DcOrder>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);

            Add = new RelayCommand(ActionAdd, CanAdd);

            Edit = new RelayCommand<DcOrder>(order =>
            {
                //修改提示
                if (!PMSDialogService.ShowYesNo("请问", "确定要【修改】这个订单吗？"))
                    return;
                PMSHelper.ViewModels.OrderEdit.SetEdit(order);
                NavigationService.GoTo(PMSViews.OrderEdit);
            }, CanEdit);

            Duplicate = new RelayCommand<DcOrder>(ActionDuplicate, CanDuplicate);
            Sample = new RelayCommand<DcOrder>(ActionSample, CanSample);
            Check = new RelayCommand<DcOrder>(ActionCheck, CanCheck);
            SelectionChanged = new RelayCommand<DcOrder>(ActionSelectionChanged);
            GiveUp = new RelayCommand(() => NavigationService.GoTo(PMSViews.Order));
        }

        private bool CanSample(DcOrder arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditOrderCheck);
        }

        private void ActionSample(DcOrder obj)
        {
            VMHelper.OrderVMHelper.AddSampleFromOrder(obj);
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
                OrderCheckDialog dg = new OrderCheckDialog();
                dg.SetCurrentOrder(order.ID);
                dg.ShowDialog();
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
            return !(string.IsNullOrEmpty(SearchCustomer) && string.IsNullOrEmpty(SearchCompositionStandard) && string.IsNullOrEmpty(SearchPMINumber));
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
                var service = new OrderServiceClient();
                RecordCount = service.GetOrderCountUnCompleted2(SearchCustomer, SearchCompositionStandard, SearchPMINumber);
                service.Close();
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
                var service = new OrderServiceClient();
                var orders = service.GetOrderUnCompleted2(skip, take, SearchCustomer, SearchCompositionStandard, SearchPMINumber);
                service.Close();
                MainOrders.Clear();
                orders.ToList().ForEach(o => MainOrders.Add(o));
                CurrentOrder = MainOrders.FirstOrDefault();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

        }


        #region Proeperties
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
        public RelayCommand GiveUp { get; set; }
        #endregion
    }
}

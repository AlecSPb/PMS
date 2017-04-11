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
        /// 用于编辑后刷新订单调用
        /// </summary>
        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }


        private void InitializeProperties()
        {
            SearchCustomer = "";
            SearchCompositoinStandard = "";
            MainOrders = new ObservableCollection<DcOrder>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);

            Add = new RelayCommand(ActionAdd);

            Edit = new RelayCommand<DcOrder>(order =>
            {
                PMSHelper.ViewModels.OrderEdit.SetEdit(order);
                NavigationService.GoTo(PMSViews.OrderEdit);
            });

            Duplicate = new RelayCommand<DcOrder>(ActionDuplicate);

        }


        private void ActionDuplicate(DcOrder order)
        {
            if (order != null)
            {
                order.ID = Guid.NewGuid();
                order.CreateTime = DateTime.Now;
                order.State = "UnChecked";
                order.Priority = "Normal";
                order.DeadLine = DateTime.Now.AddDays(30);

                PMSHelper.ViewModels.OrderEdit.SetDuplicate(order);
                NavigationService.GoTo(PMSViews.OrderEdit);
            }
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.OrderEdit.SetNew();
            NavigationService.GoTo(PMSViews.OrderEdit);
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchCustomer) && string.IsNullOrEmpty(SearchCompositoinStandard));
        }

        private void ActionAll()
        {
            SearchCustomer = "";
            SearchCompositoinStandard = "";
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
                PageSize = 20;
                var service = new OrderServiceClient();
                RecordCount = service.GetOrderCountBySearch(SearchCustomer, SearchCompositoinStandard);
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
                var service = new OrderServiceClient();
                int skip, take = 0;
                skip = (PageIndex - 1) * PageSize;
                take = PageSize;
                var orders = service.GetOrderBySearchInPage(skip, take, SearchCustomer, SearchCompositoinStandard);
                MainOrders.Clear();
                orders.ToList<DcOrder>().ForEach(o => MainOrders.Add(o));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        #region Proeperties
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
        public string SearchCompositoinStandard
        {
            get { return searchCompositionStandard; }
            set
            {
                if (searchCompositionStandard == value)
                    return;
                searchCompositionStandard = value;
                RaisePropertyChanged(() => SearchCompositoinStandard);
            }
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

        #endregion
    }
}

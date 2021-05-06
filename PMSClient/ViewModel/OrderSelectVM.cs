using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.NewService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class OrderSelectVM : BaseViewModelSelect
    {
        //要转到的页面

        public OrderSelectVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
            GiveUp = new RelayCommand(() => NavigationService.GoTo(requestView));
            Select = new RelayCommand<DcOrder>(ActionSelect);

        }
        private PMSViews requestView;
        public List<string> SearchOrderStates { get; set; }

        public void RefreshData()
        {
            searchCustomer = "";
            searchPMINumber = "";
            searchCompositionStandard = "";
            searchOrderState = "";
            SetPageParametersWhenConditionChange();
        }
        /// <summary>
        /// 设置请求视图的token，返回或者选择后返回用
        /// </summary>
        /// <param name="request">请求视图的token</param>
        public void SetRequestView(PMSViews request)
        {
            requestView = request;
        }

        private void ActionSelect(DcOrder order)
        {
            if (order != null)
            {
                switch (requestView)
                {
                    case PMSViews.MaterialNeedEdit:
                        PMSHelper.ViewModels.MaterialNeedEdit.SetBySelect(order);
                        break;
                    case PMSViews.RecordTestEdit:
                        PMSHelper.ViewModels.RecordTestEdit.SetBySelectMisson(order);
                        break;
                    default:
                        break;
                }
                NavigationService.GoTo(requestView);
            }
        }
        public RelayCommand<DcOrder> Select { get; set; }

        private void ActionRefresh(Object obj)
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeProperties()
        {
            searchCustomer = "";
            searchPMINumber = "";
            searchCompositionStandard = "";
            searchOrderState = "";
            MainOrders = new ObservableCollection<DcOrder>();
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
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchPMINumber) && string.IsNullOrEmpty(SearchCompositionStandard)
                 && string.IsNullOrEmpty(SearchCustomer));
        }

        private void ActionAll()
        {
            SearchPMINumber = "";
            SearchCompositionStandard = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new NewServiceClient())
            {
                RecordCount = service.GetOrderCount(SearchCustomer, SearchCompositionStandard, SearchPMINumber, SearchOrderState);
                service.Close();
            }
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {

            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new NewServiceClient())
            {
                var orders = service.GetOrder(skip, take, SearchCustomer, SearchCompositionStandard, SearchPMINumber, SearchOrderState);
                MainOrders.Clear();
                orders.ToList().ForEach(o => MainOrders.Add(o));
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
        private ObservableCollection<DcOrder> mainOrders;
        public ObservableCollection<DcOrder> MainOrders
        {
            get { return mainOrders; }
            set { mainOrders = value; RaisePropertyChanged(nameof(MainOrders)); }
        }

        #endregion

    }
}

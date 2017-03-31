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
    public class OrderCheckVM : BaseViewModelPage
    {
        public OrderCheckVM()
        {
            Messenger.Default.Register<MsgObject>(this, VToken.OrderCheckRefresh, ActionRefresh);


            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        private void ActionRefresh(MsgObject obj)
        {
            SetPageParametersWhenConditionChange();
        }


        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
            base.Cleanup();
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
            Check = new RelayCommand<MainService.DcOrder>(ActionCheck);

        }

        private void ActionCheck(DcOrder order)
        {
            if (order != null)
            {
                MsgObject msg = new MsgObject();
                msg.MsgToken = VToken.OrderCheckEdit;
                msg.MsgModel = new ModelObject() { IsNew = false, Model = order };
                NavigationService.GoTo(msg);
            }
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
            PageIndex = 1;
            PageSize = 20;
            var service = new OrderServiceClient();
            RecordCount = service.GetOrderCountBySearch(SearchCustomer, SearchCompositoinStandard);
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            var service = new OrderServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var orders = service.GetOrderBySearchInPage(skip, take, SearchCustomer, SearchCompositoinStandard);
            MainOrders.Clear();
            orders.ToList<DcOrder>().ForEach(o => MainOrders.Add(o));
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
        public RelayCommand<DcOrder> Check { get; private set; }

        #endregion
    }
}

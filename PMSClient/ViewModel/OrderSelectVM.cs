using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class OrderSelectVM : BaseViewModelPage
    {
        //要转到的页面

        public OrderSelectVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
            SelectOrder = new RelayCommand<DcOrder>(ActionSelectOrder);

        }
        private VToken sendTo;
        /// <summary>
        /// 设置关键值
        /// </summary>
        /// <param name="sendTo">发送选择后的对象到</param>
        /// <param name="giveUp">放弃后回到</param>
        public void SetKeyProeprties(VToken sendTo, VToken giveUp)
        {
            this.sendTo = sendTo;
            GiveUp = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { NavigateTo = giveUp }));
        }

        private void ActionSelectOrder(DcOrder order)
        {
            if (order != null)
            {
                var model = new MsgObject();
                model.NavigateTo = sendTo;
                model.MsgModel = new ModelObject() { Model = order };
                NavigationService.GoTo(model);
            }
        }
        public RelayCommand<DcOrder> SelectOrder { get; set; }
        public RelayCommand GiveUp { get; set; }
        private void ActionRefresh(Object obj)
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

    }
}

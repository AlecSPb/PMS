using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;
using PMSDesktopClient.ServiceReference;
using System.Collections.ObjectModel;
using System.Windows;

namespace PMSDesktopClient.ViewModel
{
    public class OrderVM : ViewModelBase
    {
        public OrderVM()
        {
            Messenger.Default.Register<Object>(this,"RefreshOrder", ActionRefresh);


            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        private void ActionRefresh(Object obj)
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
            Navigate = new RelayCommand(() => NavigationService.GoTo("NavigationView"));
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);

            Add = new RelayCommand(ActionAdd);

            EditWithParameter = new RelayCommand<DcOrder>(order =>
            {
                MessageObject obj = new MessageObject();
                obj.ViewName = "OrderEditView";
                obj.IsAdd = false;
                obj.ModelObject = order;
                NavigationService.GoToWithParameter(obj);
            });

            Delete = new RelayCommand<ServiceReference.DcOrder>(ActionDelete);


            Duplicate = new RelayCommand<ServiceReference.DcOrder>(ActionDuplicate);

            Check = new RelayCommand<ServiceReference.DcOrder>(ActionCheck);

        }

        private void ActionCheck(DcOrder obj)
        {
            if (obj!=null)
            {
                MessageObject msg = new MessageObject();
                msg.ViewName = "OrderCheckEditView";
                msg.IsAdd = false;
                msg.ModelObject = obj;
                NavigationService.GoToWithParameter(msg);
            }
        }

        private void ActionDuplicate(DcOrder obj)
        {
            if (obj!=null)
            {
                obj.PlanVHPs = null;
                obj.ID = Guid.NewGuid();
                obj.CreateTime = DateTime.Now;
                var service = new OrderServiceClient();
                service.AddOrder(obj);
                SetPageParametersWhenConditionChange();
            }
        }

        private void ActionAdd()
        {
            var dcOrder = new DcOrder();
            dcOrder.ID = Guid.NewGuid();
            dcOrder.CustomerName = "Midsummer";
            dcOrder.PO = DateTime.Now.ToString("yyMMdd");
            dcOrder.PMIWorkingNumber = DateTime.Now.ToString("yyMMdd");
            dcOrder.ProductType = "Target";
            dcOrder.Dimension = "230mm OD x  4mm";
            dcOrder.DimensionDetails = "None";
            dcOrder.SampleNeed = "无需样品";
            dcOrder.MinimumAcceptDefect = "通常";
            dcOrder.Reviewer = "xs.zhou";
            dcOrder.PolicyContent = "";
            dcOrder.PolicyType = "VHP";
            dcOrder.PolicyMaker = "xs.zhou";

            dcOrder.Purity = "99.99";
            dcOrder.DeadLine = DateTime.Now.AddDays(30);
            dcOrder.ReviewDate = DateTime.Now;
            dcOrder.PolicyMakeDate = DateTime.Now;
            dcOrder.State = "UnChecked";
            dcOrder.Priority = "Normal";
            dcOrder.CompositionOriginal = "CuGaSe2";
            dcOrder.CompositionStandard = "Cu25Ga25Se50";
            dcOrder.CompositoinAbbr = "CuGaSe";
            dcOrder.Creator = "xs.zhou";
            dcOrder.CreateTime = DateTime.Now;
            dcOrder.ProductType = "Target";
            dcOrder.ReviewPassed = true;
            dcOrder.Quantity = 1;
            dcOrder.QuantityUnit = "片";

            MessageObject obj = new MessageObject();
            obj.ViewName = "OrderEditView";
            obj.IsAdd = true;
            obj.ModelObject = dcOrder;
            NavigationService.GoToWithParameter(obj);

        }

        private void ActionDelete(DcOrder obj)
        {
            if (obj!=null)
            {
                if (MessageBox.Show("you want to delete it?","warning",MessageBoxButton.YesNo)==MessageBoxResult.Yes)
                {
                    var service = new OrderServiceClient();
                    service.DeleteOrder(obj.ID);
                    SetPageParametersWhenConditionChange();
                }
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


        #region PagingProperties
        private int pageIndex;
        public int PageIndex
        {
            get { return pageIndex; }
            set
            {
                pageIndex = value;
                RaisePropertyChanged(nameof(PageIndex));
            }
        }

        private int pageSize;
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
                RaisePropertyChanged(nameof(PageSize));
            }
        }

        private int recordCount;
        public int RecordCount
        {
            get { return recordCount; }
            set
            {
                recordCount = value;
                RaisePropertyChanged(nameof(RecordCount));
            }
        }
        public RelayCommand PageChanged { get; private set; }
        #endregion

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
        public RelayCommand Navigate { get; private set; }
        public RelayCommand Search { get; private set; }
        public RelayCommand All { get; set; }
        public RelayCommand Add { get; private set; }
        public RelayCommand<DcOrder> EditWithParameter { get; private set; }

        public RelayCommand<DcOrder> Delete { get; private set; }

        public RelayCommand<DcOrder> Duplicate { get; private set; }

        public RelayCommand<DcOrder> Check { get;private  set; }

        #endregion
    }
}

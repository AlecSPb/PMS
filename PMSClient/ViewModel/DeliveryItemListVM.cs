using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using PMSClient.MainService;
using GalaSoft.MvvmLight.Messaging;
//using bt = BarTender;



namespace PMSClient.ViewModel
{
    public class DeliveryItemListVM : BaseViewModelPage
    {
        public DeliveryItemListVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        private void InitializeProperties()
        {
            searchProductID = "";
            searchCompositionStd = "";
            searchCustomer = "";
            DeliveryItemExtras = new ObservableCollection<DcDeliveryItemExtra>();
        }

        public void SetSearch(string vhpnumber)
        {
            SearchProductID = vhpnumber;
            SearchCompositionStd = "";
            SearchCustomer = "";
            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            SelectionChanged = new RelayCommand<DcDeliveryItemExtra>(ActionSelectionChanged);
            SearchRecordTest = new RelayCommand<DcDeliveryItemExtra>(ActionSearchRecordTest);
            SearchDelivery = new RelayCommand<DcDeliveryItemExtra>(ActionSearchDelivery);
            GiveUp = new RelayCommand(ActionGiveUp);
            Output = new RelayCommand(ActionOutput);
        }

        private void ActionOutput()
        {
            //TODO:添加Excel导出功能
            PMSDialogService.Show("这个功能还没有做");
        }

        private void ActionGiveUp()
        {
            NavigationService.GoTo(PMSViews.Delivery);
        }

        private void ActionSearchDelivery(DcDeliveryItemExtra model)
        {
            if (model != null)
            {
                PMSHelper.ViewModels.Delivery.SetSearch(model.Delivery.DeliveryName);
                NavigationService.GoTo(PMSViews.Delivery);
            }
        }

        private void ActionSearchRecordTest(DcDeliveryItemExtra model)
        {
            if (model != null)
            {
                PMSHelper.ViewModels.RecordTest.SetSearch("", model.DeliveryItem.ProductID);
                NavigationService.GoTo(PMSViews.RecordTest);
            }
        }

        private void ActionSelectionChanged(DcDeliveryItemExtra model)
        {
            if (model != null)
            {
                CurrentDeliveryItemExtra = model;
            }
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }
        private void ActionAll()
        {
            SearchProductID = SearchCompositionStd =SearchCustomer = "";
            SetPageParametersWhenConditionChange();
        }


        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            var service = new DeliveryServiceClient();
            RecordCount = service.GetDeliveryItemExtraCount(SearchProductID, SearchCompositionStd, SearchCustomer);
            service.Close();
            ActionPaging();
        }
        private void ActionPaging()
        {

            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new DeliveryServiceClient();
            var models = service.GetDeliveryItemExtra(skip, take, SearchProductID, SearchCompositionStd,SearchCustomer);
            service.Close();
            DeliveryItemExtras.Clear();
            models.ToList().ForEach(o => DeliveryItemExtras.Add(o));

            CurrentDeliveryItemExtra = DeliveryItemExtras.FirstOrDefault();
        }
        #region Properties
        public ObservableCollection<DcDeliveryItemExtra> DeliveryItemExtras { get; set; }

        private string searchCompositionStd;

        public string SearchCompositionStd
        {
            get { return searchCompositionStd; }
            set { searchCompositionStd = value; RaisePropertyChanged(nameof(SearchCompositionStd)); }
        }
        private string searchProductID;

        public string SearchProductID
        {
            get { return searchProductID; }
            set { searchProductID = value; RaisePropertyChanged(nameof(SearchProductID)); }
        }

        private string searchCustomer;

        public string SearchCustomer
        {
            get { return searchCustomer; }
            set { searchCustomer = value; RaisePropertyChanged(nameof(SearchCustomer)); }
        }
        #endregion
        #region Commands
        private DcDeliveryItemExtra currentDeliveryItemExtra;

        public DcDeliveryItemExtra CurrentDeliveryItemExtra
        {
            get { return currentDeliveryItemExtra; }
            set { currentDeliveryItemExtra = value; RaisePropertyChanged(nameof(CurrentDeliveryItemExtra)); }
        }
        public RelayCommand<DcDeliveryItemExtra> SelectionChanged { get; set; }
        public RelayCommand<DcDeliveryItemExtra> SearchRecordTest { get; set; }
        public RelayCommand<DcDeliveryItemExtra> SearchDelivery { get; set; }

        public RelayCommand GiveUp { get; set; }

        public RelayCommand Output { get; set; }
        #endregion



    }
}

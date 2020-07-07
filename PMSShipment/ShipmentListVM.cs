using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSShipment.VMHelper;
using PMSShipment.TCB;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSCommon;
using GalaSoft.MvvmLight.Messaging;

namespace PMSShipment
{
    public class ShipmentListVM : BaseViewModelPage
    {
        public ShipmentListVM()
        {
            InitializeProperties();
            InitializeCommands();

            SetPageParametersWhenConditionChange();

            Messenger.Default.Register<NotificationMessage>(this, "Refresh", ActionDo);
        }

        private void ActionDo(NotificationMessage obj)
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeProperties()
        {
            searchProductID = "";
            searchComposition = "";
            searchCustomer = "";
            searchBondingPO = searchPO = "";
            trackState = "";
            DeliveryItemExtras = new ObservableCollection<DcDeliveryItemExtra>();
            TrackStates = new List<string>();
            PMSMethods.SetListDS<PMSCommon.DeliveryItemTCBState>(TrackStates);
            TrackStates.Add("");
        }

        public List<string> TrackStates { get; set; }

        public void SetSearch(string vhpnumber)
        {
            SearchProductID = vhpnumber;
            SearchComposition = "";
            SearchCustomer = "";
            SearchBondingPO = "";
            SearchPO = "";
            TrackState = "";
            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            SelectionChanged = new RelayCommand<DcDeliveryItemExtra>(ActionSelectionChanged);
            Set = new RelayCommand<DcDeliveryItemExtra>(ActionSet);
        }

        private void ActionSet(DcDeliveryItemExtra model)
        {
            if (model == null) return;
            var win = new SetWindow();
            var vm = new SetVM();
            vm.SetCurrentDeliveryItem(model.DeliveryItem);
            win.DataContext = vm;
            win.ShowDialog();
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
            SearchProductID = SearchComposition = SearchCustomer = SearchBondingPO = SearchPO = TrackState = "";
            SetPageParametersWhenConditionChange();
        }


        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            var service = new TCBServiceClient();
            RecordCount = service.GetDeliveryItemExtraCount(SearchProductID, SearchComposition, SearchPO, SearchCustomer, SearchBondingPO, TrackState);
            service.Close();
            ActionPaging();
        }
        private void ActionPaging()
        {

            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new TCBServiceClient();
            var models = service.GetDeliveryItemExtra(skip, take, SearchProductID, SearchComposition, SearchPO, SearchCustomer, SearchBondingPO, TrackState);
            service.Close();
            DeliveryItemExtras.Clear();
            models.ToList().ForEach(o => DeliveryItemExtras.Add(o));

            CurrentDeliveryItemExtra = DeliveryItemExtras.FirstOrDefault();
        }
        #region Properties
        public ObservableCollection<DcDeliveryItemExtra> DeliveryItemExtras { get; set; }

        private string searchComposition;

        public string SearchComposition
        {
            get { return searchComposition; }
            set { searchComposition = value; RaisePropertyChanged(nameof(SearchComposition)); }
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

        private string searchPO;

        public string SearchPO
        {
            get { return searchPO; }
            set { searchPO = value; RaisePropertyChanged(nameof(SearchPO)); }
        }
        private string searchBondingPO;

        public string SearchBondingPO
        {
            get { return searchBondingPO; }
            set { searchBondingPO = value; RaisePropertyChanged(nameof(SearchBondingPO)); }
        }
        private string trackState;
        public string TrackState
        {
            get { return trackState; }
            set { trackState = value; RaisePropertyChanged(nameof(TrackState)); }
        }
        #endregion
        #region Commands
        private DcDeliveryItemExtra currentDeliveryItem;

        public DcDeliveryItemExtra CurrentDeliveryItemExtra
        {
            get { return currentDeliveryItem; }
            set { currentDeliveryItem = value; RaisePropertyChanged(nameof(CurrentDeliveryItemExtra)); }
        }
        public RelayCommand<DcDeliveryItemExtra> SelectionChanged { get; set; }
        public RelayCommand<DcDeliveryItemExtra> Set { get; set; }


        #endregion
    }
}

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
            DeliveryItemExtras = new ObservableCollection<DcDeliveryItemExtra>();
        }
        private void InitializeCommands()
        {
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }
        private void ActionAll()
        {
            SearchProductID = SearchCompositionStd = "";
            SetPageParametersWhenConditionChange();
        }


        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new DeliveryServiceClient();
            RecordCount = service.GetDeliveryItemExtraCount(SearchProductID, SearchCompositionStd);

            service.Close();
            ActionPaging();
        }
        private void ActionPaging()
        {

            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new DeliveryServiceClient();
            var models = service.GetDeliveryItemExtra(skip, take, SearchProductID, SearchCompositionStd);
            service.Close();
            DeliveryItemExtras.Clear();
            models.ToList().ForEach(o => DeliveryItemExtras.Add(o));
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
        #endregion
        #region Commands

        #endregion



    }
}

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

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeProperties()
        {
            searchDeliveryName = "";
            DeliveryIteExtras = new ObservableCollection<DcDeliveryItemExtra>();
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
            SetPageParametersWhenConditionChange();
        }


        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new DeliveryServiceClient();
            RecordCount = service.GetDeliveryItemExtraCount(SearchDeliveryName);
            service.Close();
            ActionPaging();
        }
        private void ActionPaging()
        {

            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new DeliveryServiceClient();
            var models = service.GetDeliveryItemExtra(skip, take, SearchDeliveryName);
            service.Close();
            DeliveryItemExtras.Clear();
            models.ToList().ForEach(o => DeliveryItemExtras.Add(o));
        }
        #region Properties
        public ObservableCollection<DcDeliveryItemExtra> DeliveryItemExtras { get; set; }

        private string searchDeliveryName;

        public string SearchDeliveryName
        {
            get { return searchDeliveryName; }
            set { searchDeliveryName = value; RaisePropertyChanged(nameof(SearchDeliveryName)); }
        }

        #endregion
        #region Commands
  
        #endregion



    }
}

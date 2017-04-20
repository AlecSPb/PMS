using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;

namespace PMSClient.ViewModel
{
    public class RecordTestSelectVM : BaseViewModelSelect
    {

        public RecordTestSelectVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }
        private PMSViews requestView;
        public void SetRequestView(PMSViews view)
        {
            requestView = view;
        }
        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Select = new RelayCommand<DcRecordTest>(ActionSelect);
        }

        private void GoBack()
        {
            NavigationService.GoTo(requestView);
        }

        private bool CanSearch()
        {
            return !string.IsNullOrEmpty(SearchProductID) || !string.IsNullOrEmpty(SearchCompositionStd);
        }

        private void ActionAll()
        {
            SearchProductID = SearchCompositionStd = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionSelect(DcRecordTest model)
        {
            if (model!=null)
            {
                switch (requestView)
                {
                    case PMSViews.RecordBondingEdit:
                        PMSHelper.ViewModels.RecordBondingEdit.SetBySelect(model);
                        break;
                    case PMSViews.ProductEdit:
                        PMSHelper.ViewModels.ProductEdit.SetBySelect(model);
                        break;
                    case PMSViews.DeliveryItemEdit:
                        PMSHelper.ViewModels.DeliveryItemEdit.SetBySelect(model);
                        break;
                    default:
                        break;
                }
                GoBack();
            }
        }

        private void InitializeProperties()
        {
            RecordProducts = new ObservableCollection<DcRecordTest>();
            SearchCompositionStd = searchProductID = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            using (var service = new RecordTestServiceClient())
            {
                RecordCount = service.GetRecordTestCountBySearchInPage(SearchProductID, SearchCompositionStd);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new RecordTestServiceClient())
            {
                var orders = service.GetRecordTestBySearchInPage(skip, take, SearchProductID, SearchCompositionStd);
                RecordProducts.Clear();
                orders.ToList().ForEach(o => RecordProducts.Add(o));
            }
        }
        #region Commands
        public RelayCommand<DcRecordTest> Select { get; set; }

        #endregion

        #region Properties
        private string searchProductID;
        public string SearchProductID
        {
            get { return searchProductID; }
            set
            {
                if (searchProductID == value)
                    return;
                searchProductID = value;
                RaisePropertyChanged(() => SearchProductID);
            }
        }
        private string searchCompositionStd;
        public string SearchCompositionStd
        {
            get { return searchCompositionStd; }
            set
            {
                if (searchCompositionStd == value)
                    return;
                searchCompositionStd = value;
                RaisePropertyChanged(() => SearchCompositionStd);
            }
        }

        public ObservableCollection<DcRecordTest> RecordProducts { get; set; }
        #endregion
    }
}

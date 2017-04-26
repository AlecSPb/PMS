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
using PMSClient.ViewModel.Model;

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
            Select = new RelayCommand<RecordTestExtra>(ActionSelect);
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

        private void ActionSelect(RecordTestExtra model)
        {
            if (model!=null)
            {
                switch (requestView)
                {
                    case PMSViews.RecordBondingEdit:
                        PMSHelper.ViewModels.RecordBondingEdit.SetBySelect(model.RecordTest);
                        break;
                    case PMSViews.ProductEdit:
                        PMSHelper.ViewModels.ProductEdit.SetBySelect(model.RecordTest);
                        break;
                    case PMSViews.DeliveryItemEdit:
                        PMSHelper.ViewModels.DeliveryItemEdit.SetBySelect(model.RecordTest);
                        break;
                    default:
                        break;
                }
                GoBack();
            }
        }

        private void InitializeProperties()
        {
            RecordProductExtras = new ObservableCollection<RecordTestExtra>();
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
                RecordProductExtras.Clear();
                orders.ToList().ForEach(o => RecordProductExtras.Add(new Model.RecordTestExtra { RecordTest=o}));
            }
        }
        #region Commands
        public RelayCommand<RecordTestExtra> Select { get; set; }

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

        public ObservableCollection<RecordTestExtra> RecordProductExtras { get; set; }
        #endregion
    }
}

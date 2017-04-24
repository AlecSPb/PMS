using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ViewModel
{
    public class RecordBondingSelectVM : BaseViewModelSelect
    {
        public RecordBondingSelectVM()
        {
            RecordBondings = new ObservableCollection<DcRecordBonding>();
            GiveUp = new RelayCommand(GoBack);
            InitializeCommands();
            searchCompositionStd = searchProductID = "";
            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            Select = new RelayCommand<MainService.DcRecordBonding>(ActionSelect);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
        }
        private PMSViews requestView;
        public void SetRequestView(PMSViews view)
        {
            requestView = view;
        }

        private void GoBack()
        {
            NavigationService.GoTo(requestView);
        }
        private void ActionSelect(DcRecordBonding model)
        {
            if (model != null)
            {
                switch (requestView)
                {
                    case PMSViews.ProductEdit:
                        PMSHelper.ViewModels.ProductEdit.SetBySelect(model);
                        break;
                    default:
                        break;
                }
                GoBack();
            }
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

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            using (var service = new RecordBondingServiceClient())
            {
                RecordCount = service.GetRecordBondingCount(SearchProductID, SearchCompositionStd);
            }

            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;

            using (var service = new RecordBondingServiceClient())
            {
                var orders = service.GetRecordBondings(skip, take, SearchProductID, SearchCompositionStd);
                RecordBondings.Clear();
                orders.ToList().ForEach(o => RecordBondings.Add(o));
            }
        }


        public ObservableCollection<DcRecordBonding> RecordBondings { get; set; }

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


        public RelayCommand<DcRecordBonding> Select { get; set; }


    }
}

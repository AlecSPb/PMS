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
    public class RecordBondingVM : BaseViewModelPage
    {
        public RecordBondingVM()
        {
            RecordBondings = new ObservableCollection<DcRecordBonding>();

            InitializeCommands();
            searchCompositionStd = searchProductID = "";
            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            Add = new RelayCommand(ActionAdd, CanAdd);
            Detail = new RelayCommand<DcRecordBonding>(ActionDetail);
            Edit = new RelayCommand<DcRecordBonding>(ActionEdit, CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
        }

        private bool CanEdit(DcRecordBonding arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑绑定记录");
        }

        private void ActionEdit(DcRecordBonding model)
        {
            if (model!=null)
            {
                PMSHelper.ViewModels.RecordBondingEdit.SetEdit(model);
                NavigationService.GoTo(PMSViews.RecordBondingEdit);
            }
        }

        private void ActionDetail(DcRecordBonding obj)
        {
            throw new NotImplementedException();
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

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑绑定记录");
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.RecordBondingEdit.SetNew();
            NavigationService.GoTo(PMSViews.RecordBondingEdit);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
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


        public RelayCommand Add { get; set; }
        public RelayCommand<DcRecordBonding> Detail { get; set; }
        public RelayCommand<DcRecordBonding> Edit { get; set; }


    }
}

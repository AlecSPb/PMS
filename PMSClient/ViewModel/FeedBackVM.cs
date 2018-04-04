using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using PMSClient.ExtraService;

namespace PMSClient.ViewModel
{
    public class FeedBackVM:BaseViewModelPage
    {
        public FeedBackVM()
        {
            searchProductID = searchComposition = searchCustomer = "";
            FeedBacks = new ObservableCollection<DcFeedBack>();

            InitializeCommands();

            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcFeedBack>(ActionEdit, CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
        }

        private void ActionAll()
        {
            SearchProductID = SearchComposition = SearchCustomer = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private bool CanEdit(DcFeedBack arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditFeedback);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditFeedback);
        }

        private void ActionEdit(DcFeedBack model)
        {
            PMSHelper.ViewModels.FeedBackEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.FeedBackEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.FeedBackEdit.SetNew();
            NavigationService.GoTo(PMSViews.FeedBackEdit);
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }


        private string searchProductID;
        public string SearchProductID
        {
            get { return searchProductID; }
            set { searchProductID = value; RaisePropertyChanged(nameof(SearchProductID)); }
        }
        private string searchComposition;
        public string SearchComposition
        {
            get { return searchComposition; }
            set { searchComposition = value; RaisePropertyChanged(nameof(SearchComposition)); }
        }
        private string searchCustomer;
        public string SearchCustomer
        {
            get { return searchCustomer; }
            set { searchCustomer = value; RaisePropertyChanged(nameof(SearchCustomer)); }
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new FeedBackServiceClient())
            {
                RecordCount = service.GetFeedBackCount(SearchProductID, SearchComposition, SearchCustomer);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new FeedBackServiceClient())
            {
                var orders = service.GetFeedBack(skip,take,SearchProductID, SearchComposition, SearchCustomer);
                FeedBacks.Clear();
                orders.ToList().ForEach(o => FeedBacks.Add(o));
            }
        }
        #region Commands
        public ObservableCollection<DcFeedBack> FeedBacks { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcFeedBack> Edit { get; set; }

        #endregion

    }
}

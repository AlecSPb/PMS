using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.FailureService;

namespace PMSClient.ViewModel
{
    public class FailureVM : BaseViewModelPage
    {
        public FailureVM()
        {
            searchStage = "";
            Failures = new ObservableCollection<DcFailure>();

            InitializeCommands();

            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcFailure>(ActionEdit, CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
        }

        private void ActionAll()
        {
            SearchStage = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private bool CanEdit(DcFailure arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditFeedback);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditFeedback);
        }

        private void ActionEdit(DcFailure model)
        {
            PMSHelper.ViewModels.FailureEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.FeedBackEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.FailureEdit.SetNew();
            NavigationService.GoTo(PMSViews.FeedBackEdit);
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }


        private string searchStage;
        public string SearchStage
        {
            get { return searchStage; }
            set { searchStage = value; RaisePropertyChanged(nameof(SearchStage)); }
        }


        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new FailureServiceClient())
            {
                RecordCount = service.GetFailuresCount(SearchStage);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new FailureServiceClient())
            {
                var orders = service.GetFailures(skip, take, SearchStage);
                Failures.Clear();
                orders.ToList().ForEach(o => Failures.Add(o));
            }
        }
        #region Commands
        public ObservableCollection<DcFailure> Failures { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcFailure> Edit { get; set; }

        #endregion

    }
}

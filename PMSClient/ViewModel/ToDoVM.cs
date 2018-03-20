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
    public class ToDoVM : BaseViewModelPage
    {
        public ToDoVM()
        {
            searchTitle = searchPersonInCharge = "";
            ToDoList = new ObservableCollection<DcToDo>();
            CurrentToDoItem = new DcToDo();

            InitializeCommands();

            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcToDo>(ActionEdit, CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            SelectionChanged = new RelayCommand<DcToDo>(ActionSelectionChanged);
        }

        private void ActionSelectionChanged(DcToDo model)
        {
            CurrentToDoItem = model;
        }

        private void ActionAll()
        {
            SearchTitle = SearchPersonInCharge = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private bool CanEdit(DcToDo arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditFeedback);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditFeedback);
        }

        private void ActionEdit(DcToDo model)
        {
            PMSHelper.ViewModels.ToDoEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.ToDoEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.ToDoEdit.SetNew();
            NavigationService.GoTo(PMSViews.ToDoEdit);
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }


        private string searchTitle;
        public string SearchTitle
        {
            get { return searchTitle; }
            set { searchTitle = value; RaisePropertyChanged(nameof(SearchTitle)); }
        }
        private string searchPersonInCharge;
        public string SearchPersonInCharge
        {
            get { return searchPersonInCharge; }
            set { searchPersonInCharge = value; RaisePropertyChanged(nameof(SearchPersonInCharge)); }
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 50;
            using (var service = new ToDoServiceClient())
            {
                RecordCount = service.GetToDoCount(SearchTitle, SearchPersonInCharge);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new ToDoServiceClient())
            {
                var orders = service.GetToDo(SearchTitle, SearchPersonInCharge, skip, take);
                ToDoList.Clear();
                orders.ToList().ForEach(o => ToDoList.Add(o));
                CurrentToDoItem = orders.FirstOrDefault();
            }
        }
        #region Commands

        private DcToDo currentToDoItem;

        public DcToDo CurrentToDoItem
        {
            get { return currentToDoItem; }
            set { currentToDoItem = value; RaisePropertyChanged(nameof(CurrentToDoItem)); }
        }



        public ObservableCollection<DcToDo> ToDoList { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcToDo> Edit { get; set; }
        public RelayCommand<DcToDo> SelectionChanged { get; set; }
        #endregion

    }
}

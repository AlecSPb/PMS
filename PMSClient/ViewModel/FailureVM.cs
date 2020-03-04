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
            searchStage = searchComposition = searchProductID = "";
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
            Duplicate = new RelayCommand<DcFailure>(ActionDuplicate, CanDuplicate);
        }

        private void ActionDuplicate(DcFailure obj)
        {
            if (PMSDialogService.ShowYesNo("请问", "确定复用这条记录？"))
            {
                if (obj != null)
                {
                    PMSHelper.ViewModels.FailureEdit.SetDuplicate(obj);
                    NavigationService.GoTo(PMSViews.FailureEdit);
                }
            }

        }

        private bool CanDuplicate(DcFailure arg)
        {
            return PMSHelper.CurrentSession.IsOKInGroup(groupnames);
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

        private string[] groupnames = { "管理员", "制粉组", "热压组", "加工组", "测试组", "质量组", "发货组", "发货专员", "生产经理", "仓库专员", "熔铸部门", "统筹组" };
        private bool CanEdit(DcFailure arg)
        {
            return PMSHelper.CurrentSession.IsOKInGroup(groupnames);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsOKInGroup(groupnames);
        }

        private void ActionEdit(DcFailure model)
        {
            PMSHelper.ViewModels.FailureEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.FailureEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.FailureEdit.SetNew();
            NavigationService.GoTo(PMSViews.FailureEdit);
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
                var orders = service.GetFailuresBySearch(skip, take,
                    SearchProductID, SearchComposition, SearchStage);
                Failures.Clear();
                orders.ToList().ForEach(o => Failures.Add(o));
            }
        }
        #region Commands
        public ObservableCollection<DcFailure> Failures { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcFailure> Edit { get; set; }
        public RelayCommand<DcFailure> Duplicate { get; set; }
        #endregion

    }
}

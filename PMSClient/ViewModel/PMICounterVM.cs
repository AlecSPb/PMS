using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.ExtraService;

namespace PMSClient.ViewModel
{
    public class PMICounterVM : BaseViewModelPage
    {
        public PMICounterVM()
        {
            searchItemName=searchItemGroup = "";
            PMICounters = new ObservableCollection<DcPMICounter>();

            InitializeCommands();

            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcPMICounter>(ActionEdit, CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            Duplicate = new RelayCommand<DcPMICounter>(ActionDuplicate, CanDuplicate);
        }

        private void ActionDuplicate(DcPMICounter obj)
        {
            if (PMSDialogService.ShowYesNo("请问", "确定复用这条记录？"))
            {
                if (obj != null)
                {
                    PMSHelper.ViewModels.PMICounterEdit.SetDuplicate(obj);
                    NavigationService.GoTo(PMSViews.PMICounterEdit);
                }
            }

        }

        private bool CanDuplicate(DcPMICounter arg)
        {
            return PMSHelper.CurrentSession.IsOKGroup(groupnames);
        }

        private void ActionAll()
        {
            SearchItemName=SearchItemGroup = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private string[] groupnames = { "管理员", "制粉组", "热压组", "加工组", "测试组", "质量组", "发货组", "发货专员", "生产经理", "仓库专员", "熔铸部门" };
        private bool CanEdit(DcPMICounter arg)
        {
            return PMSHelper.CurrentSession.IsOKGroup(groupnames);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsOKGroup(groupnames);
        }

        private void ActionEdit(DcPMICounter model)
        {
            PMSHelper.ViewModels.PMICounterEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.PMICounterEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.PMICounterEdit.SetNew();
            NavigationService.GoTo(PMSViews.PMICounterEdit);
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }


        private string searchItemName;
        public string SearchItemName
        {
            get { return searchItemName; }
            set { searchItemName = value; RaisePropertyChanged(nameof(SearchItemName)); }
        }

        private string searchItemGroup;
        public string SearchItemGroup
        {
            get { return searchItemGroup; }
            set { searchItemGroup = value; RaisePropertyChanged(nameof(SearchItemGroup)); }
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new PMICounterServiceClient())
            {
                RecordCount = service.GetPMICounterCount(SearchItemGroup,SearchItemName);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new PMICounterServiceClient())
            {
                var orders = service.GetPMICounter(SearchItemGroup, SearchItemName, skip, take);
                PMICounters.Clear();
                orders.ToList().ForEach(o => PMICounters.Add(o));
            }
        }
        #region Commands
        public ObservableCollection<DcPMICounter> PMICounters { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcPMICounter> Edit { get; set; }
        public RelayCommand<DcPMICounter> Duplicate { get; set; }
        #endregion

    }
}

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
    public class RemainInventoryVM : BaseViewModelPage
    {
        public RemainInventoryVM()
        {
            searchProductID =searchComposition= "";
            RemainInventorys = new ObservableCollection<DcRemainInventory>();

            InitializeCommands();

            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcRemainInventory>(ActionEdit, CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            Duplicate = new RelayCommand<DcRemainInventory>(ActionDuplicate, CanDuplicate);
        }

        private void ActionDuplicate(DcRemainInventory obj)
        {
            if (PMSDialogService.ShowYesNo("请问", "确定复用这条记录？"))
            {
                if (obj != null)
                {
                    PMSHelper.ViewModels.RemainInventoryEdit.SetDuplicate(obj);
                    NavigationService.GoTo(PMSViews.RemainInventoryEdit);
                }
            }

        }

        private bool CanDuplicate(DcRemainInventory arg)
        {
            return PMSHelper.CurrentSession.IsOKGroup(groupnames);
        }

        private void ActionAll()
        {
            SearchProductID = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private string[] groupnames = { "管理员", "制粉组", "热压组", "加工组", "测试组", "质量组", "发货组", "发货专员", "生产经理", "仓库专员", "熔铸部门" };
        private bool CanEdit(DcRemainInventory arg)
        {
            return PMSHelper.CurrentSession.IsOKGroup(groupnames);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsOKGroup(groupnames);
        }

        private void ActionEdit(DcRemainInventory model)
        {
            PMSHelper.ViewModels.RemainInventoryEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.RemainInventoryEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.RemainInventoryEdit.SetNew();
            NavigationService.GoTo(PMSViews.RemainInventoryEdit);
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

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new RemainInventoryServiceClient())
            {
                RecordCount = service.GetRemainInventoryCounter(SearchProductID,SearchComposition);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new RemainInventoryServiceClient())
            {
                var orders = service.GetRemainInventories(SearchProductID, SearchComposition, skip, take);
                RemainInventorys.Clear();
                orders.ToList().ForEach(o => RemainInventorys.Add(o));
            }
        }
        #region Commands
        public ObservableCollection<DcRemainInventory> RemainInventorys { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcRemainInventory> Edit { get; set; }
        public RelayCommand<DcRemainInventory> Duplicate { get; set; }
        #endregion

    }
}

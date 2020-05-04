using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using PMSClient.ConsumableService;


namespace PMSClient.ViewModel
{
    public class ConsumableInventoryVM : BaseViewModelPage
    {
        public ConsumableInventoryVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }


        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcConsumableInventory>(ActionEdit, CanEdit);
            Duplicate = new RelayCommand<DcConsumableInventory>(ActionDuplicate, CanDuplicate);
            QuickChange = new RelayCommand<DcConsumableInventory>(ActionQuickChange, CanQuickChange);
            ShowHistory = new RelayCommand<DcConsumableInventory>(ActionShowHistory);
            ShowCountHistory = new RelayCommand<DcConsumableInventory>(ActionShowCountHistory);
        }

        private void ActionShowCountHistory(DcConsumableInventory obj)
        {
            if (obj != null)
            {
                var dialog = new WPFControls.NormalizedDataViewer();
                dialog.SetMainStrings(obj.CountHistory);
                dialog.ShowDialog();
            }
        }

        private void ActionShowHistory(DcConsumableInventory obj)
        {
            if (obj != null)
            {
                var dialog = new WPFControls.NormalizedDataViewer();
                dialog.SetMainStrings(obj.History);
                dialog.ShowDialog();
            }
        }

        private bool CanQuickChange(DcConsumableInventory arg)
        {
            return PMSHelper.CurrentSession.IsInGroup("ConsumableInventoryQuickChange");
        }

        private string AddHistory(string history, string sign, int count)
        {
            return $"{DateTime.Today.ToString("yyMMdd")}{sign}{count};{history}";
        }
        private void ActionQuickChange(DcConsumableInventory obj)
        {
            if (obj == null) return;
            var dialog = new ToolDialog.PMICounterQuickEditDialog();
            dialog.Remark = obj.Remark;
            dialog.ShowDialog();

            obj.Remark = dialog.Remark;
            if (dialog.EditType == ToolDialog.PMICounterEditType.IsCancel)
            { return; }
            else if (dialog.EditType == ToolDialog.PMICounterEditType.IsAdd)
            {
                obj.Quantity += dialog.Counter;
                obj.History = AddHistory(obj.History, "+", dialog.Counter);
            }
            else
            {
                obj.Quantity -= dialog.Counter;
                obj.History = AddHistory(obj.History, "-", dialog.Counter);
            }

            using (var service = new ConsumableServiceClient())
            {
                service.UpdateConsumableInventory(obj);
                SetPageParametersWhenConditionChange();
            }

        }

        private bool CanDuplicate(DcConsumableInventory arg)
        {
            return PMSHelper.CurrentSession.IsInGroup("ConsumableInventoryEdit");
        }

        private void ActionDuplicate(DcConsumableInventory model)
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定复用这条记录吗？"))
            {
                return;
            }
            PMSHelper.ViewModels.ConsumableInventoryEdit.SetDuplicate(model);
            NavigationService.GoTo(PMSViews.ConsumableInventoryEdit);
        }

        private bool CanEdit(DcConsumableInventory arg)
        {
            return PMSHelper.CurrentSession.IsInGroup("ConsumableInventoryEdit");
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsInGroup("ConsumableInventoryEdit");
        }


        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchItemName));
        }

        private void ActionAll()
        {
            searchItemName = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionEdit(DcConsumableInventory model)
        {
            PMSHelper.ViewModels.ConsumableInventoryEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.ConsumableInventoryEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.ConsumableInventoryEdit.SetNew();
            NavigationService.GoTo(PMSViews.ConsumableInventoryEdit);
        }

        private void InitializeProperties()
        {
            ConsumableInventories = new ObservableCollection<DcConsumableInventory>();
            searchItemName = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new ConsumableServiceClient())
            {
                RecordCount = service.GetConsumableInventoryCount(SearchItemName);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new ConsumableServiceClient())
            {
                var orders = service.GetConsumableInventory(skip, take, SearchItemName);
                ConsumableInventories.Clear();
                orders.ToList().ForEach(o => ConsumableInventories.Add(o));
            }
        }
        #region Commands
        public RelayCommand Add { get; set; }
        public RelayCommand<DcConsumableInventory> Edit { get; set; }


        private string searchItemName;
        public string SearchItemName
        {
            get { return searchItemName; }
            set
            {
                if (searchItemName == value)
                    return;
                searchItemName = value;
                RaisePropertyChanged(() => SearchItemName);
            }
        }

        public ObservableCollection<DcConsumableInventory> ConsumableInventories { get; set; }
        #endregion
        public RelayCommand<DcConsumableInventory> Duplicate { get; set; }
        public RelayCommand<DcConsumableInventory> QuickChange { get; set; }

        public RelayCommand<DcConsumableInventory> ShowHistory { get; set; }
        public RelayCommand<DcConsumableInventory> ShowCountHistory { get; set; }

    }
}

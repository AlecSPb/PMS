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
    public class ConsumablePurchaseVM : BaseViewModelPage
    {
        public ConsumablePurchaseVM()
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
            Edit = new RelayCommand<DcConsumablePurchase>(ActionEdit, CanEdit);
            Duplicate = new RelayCommand<DcConsumablePurchase>(ActionDuplicate, CanDuplicate);
            Excel = new RelayCommand(ActionExcel);
        }

        private void ActionExcel()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定要导出全部数据吗？"))
            {
                return;
            }
            try
            {
                //年月选择对话框
                var dialog = new WPFControls.YearDateDailog(-1);

                if (dialog.ShowDialog() == false)
                {
                    return;
                }

                int year_start = dialog.YearStart;
                int month_start = dialog.MonthStart;
                int year_end = dialog.YearEnd;
                int month_end = dialog.MonthEnd;


                var excel = new ExcelOutputHelper.ExcelConsumablePurchase();
                excel.Intialize($"消耗品购买 {year_start}_{month_start} to {year_end}_{month_end}", "Data", 50);
                excel.SetParameter(year_start, month_start, year_end, month_end);
                excel.Output();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                PMSDialogService.Show(ex.Message);
            }
        }

        private bool CanDuplicate(DcConsumablePurchase arg)
        {
            return PMSHelper.CurrentSession.IsInGroup("ConsumablePurchaseEdit");
        }

        private void ActionDuplicate(DcConsumablePurchase model)
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定复用这条记录吗？"))
            {
                return;
            }
            PMSHelper.ViewModels.ConsumablePurchaseEdit.SetDuplicate(model);
            NavigationService.GoTo(PMSViews.ConsumablePurchaseEdit);
        }

        private bool CanEdit(DcConsumablePurchase arg)
        {
            return PMSHelper.CurrentSession.IsInGroup("ConsumablePurchaseEdit");
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsInGroup("ConsumablePurchaseEdit");
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

        private void ActionEdit(DcConsumablePurchase model)
        {
            PMSHelper.ViewModels.ConsumablePurchaseEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.ConsumablePurchaseEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.ConsumablePurchaseEdit.SetNew();
            NavigationService.GoTo(PMSViews.ConsumablePurchaseEdit);
        }

        private void InitializeProperties()
        {
            ConsumablePurchases = new ObservableCollection<DcConsumablePurchase>();
            searchItemName  = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new ConsumableServiceClient())
            {
                RecordCount = service.GetConsumablePurchaseCount(SearchItemName);
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
                var orders = service.GetConsumablePurchase(skip, take,SearchItemName);
                ConsumablePurchases.Clear();
                orders.ToList().ForEach(o => ConsumablePurchases.Add(o));
            }
        }
        #region Commands
        public RelayCommand Add { get; set; }
        public RelayCommand<DcConsumablePurchase> Edit { get; set; }

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

        public ObservableCollection<DcConsumablePurchase> ConsumablePurchases { get; set; }
        #endregion
        public RelayCommand<DcConsumablePurchase> Duplicate { get; set; }
        public RelayCommand Excel { get; set; }

    }
}

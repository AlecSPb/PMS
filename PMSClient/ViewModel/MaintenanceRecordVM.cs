using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.Maintainance;

namespace PMSClient.ViewModel
{
    public class MaintenanceRecordVM : BaseViewModelPage
    {
        public MaintenanceRecordVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeProperties()
        {
            searchVHPDeviceCode = searchPlanItem = "";
            Records = new ObservableCollection<DcMaintenanceRecord>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);

            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcMaintenanceRecord>(ActionEdit, CanEdit);
            Duplicate = new RelayCommand<DcMaintenanceRecord>(ActionDuplicate, CanEdit);
        }

        private void ActionDuplicate(DcMaintenanceRecord model)
        {
            if (XSHelper.XS.MessageBox.ShowYesNo("确定复用此条记录吗？"))
            {
                if (model != null)
                {
                    PMSHelper.ViewModels.MaintenanceRecordEdit.SetDuplicate(model);
                    NavigationService.GoTo(PMSViews.MaintainRecordEdit);
                }
            }
        }


        //用于任务定位调用
        public void SetSearchCondition(string composition, string pminumber)
        {
            SearchVHPDeviceCode = composition;
            //需要重新激发一下
            RaisePropertyChanged(nameof(SearchVHPDeviceCode));

            SetPageParametersWhenConditionChange();
        }


        private void ActionOnlyUnCompleted()
        {
            NavigationService.GoTo(PMSViews.MaterialInventoryInUnCompleted);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsInGroup("MaintainPlanEdit");

        }

        private bool CanEdit(DcMaintenanceRecord arg)
        {
            return PMSHelper.CurrentSession.IsInGroup("MaintainPlanEdit");

        }

        private void ActionEdit(DcMaintenanceRecord model)
        {
            if (XSHelper.XS.MessageBox.ShowYesNo("要继续修改此条维修日志记录吗？"))
            {
                if (model != null)
                {
                    PMSHelper.ViewModels.MaintenanceRecordEdit.SetEdit(model);
                    NavigationService.GoTo(PMSViews.MaintainRecordEdit);
                }
            }

        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.MaintenanceRecordEdit.SetNew();
            NavigationService.GoTo(PMSViews.MaintainRecordEdit);
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchVHPDeviceCode));
        }

        private void ActionAll()
        {
            SearchVHPDeviceCode = SearchPlanItem = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            var service = new MaintenanceServiceClient();
            RecordCount = service.GetMaintenanceRecordsCount(SearchVHPDeviceCode, SearchPlanItem);
            service.Close();
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {

            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new MaintenanceServiceClient();
            var result = service.GetMaintenanceRecords(skip, take, SearchVHPDeviceCode, SearchPlanItem);
            service.Close();
            Records.Clear();
            result.ToList().ForEach(o => Records.Add(o));
        }

        #region Proeperties
        private string searchVHPDeviceCode;
        public string SearchVHPDeviceCode
        {
            get { return searchVHPDeviceCode; }
            set
            {
                if (searchVHPDeviceCode == value)
                    return;
                searchVHPDeviceCode = value;
                RaisePropertyChanged(() => SearchVHPDeviceCode);
            }
        }
        private string searchPlanItem;
        public string SearchPlanItem
        {
            get { return searchPlanItem; }
            set
            {
                if (searchPlanItem == value)
                    return;
                searchPlanItem = value;
                RaisePropertyChanged(() => SearchPlanItem);
            }
        }


        private ObservableCollection<DcMaintenanceRecord> records;
        public ObservableCollection<DcMaintenanceRecord> Records
        {
            get { return records; }
            set { records = value; RaisePropertyChanged(nameof(Records)); }
        }

        #endregion

        #region Commands
        public RelayCommand Add { get; private set; }
        public RelayCommand<DcMaintenanceRecord> Edit { get; private set; }
        public RelayCommand<DcMaintenanceRecord> Duplicate { get; private set; }
        #endregion
    }
}

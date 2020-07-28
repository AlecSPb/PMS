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
    public class MaintenancePlanVM : BaseViewModelPage
    {
        public MaintenancePlanVM()
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
            Plans = new ObservableCollection<DcMaintenancePlan>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);

            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcMaintenancePlan>(ActionEdit, CanEdit);
            Duplicate = new RelayCommand<DcMaintenancePlan>(ActionDuplicate, CanEdit);
        }

        private void ActionDuplicate(DcMaintenancePlan model)
        {
            if (XSHelper.XS.MessageBox.ShowYesNo("确定复用此条记录吗？"))
            {
                if (model != null)
                {
                    PMSHelper.ViewModels.MaintenancePlanEdit.SetDuplicate(model);
                    NavigationService.GoTo(PMSViews.MaintainPlanEdit);
                }
            }
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsInGroup("MaintainPlanEdit");

        }

        private bool CanEdit(DcMaintenancePlan arg)
        {
            return PMSHelper.CurrentSession.IsInGroup("MaintainPlanEdit");

        }

        private void ActionEdit(DcMaintenancePlan model)
        {
            if (XSHelper.XS.MessageBox.ShowYesNo("此计划可能已经开始执行，要继续修改此条记录吗？"))
            {
                if (model != null)
                {
                    PMSHelper.ViewModels.MaintenancePlanEdit.SetEdit(model);
                    NavigationService.GoTo(PMSViews.MaintainPlanEdit);
                }
            }

        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.MaintenancePlanEdit.SetNew();
            NavigationService.GoTo(PMSViews.MaintainPlanEdit);
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
            RecordCount = service.GetMaintenancePlanCount(SearchVHPDeviceCode, SearchPlanItem);
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
            var result = service.GetMaintenancePlans(skip, take, SearchVHPDeviceCode, SearchPlanItem);
            service.Close();
            Plans.Clear();
            result.ToList().ForEach(o => Plans.Add(o));
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


        private ObservableCollection<DcMaintenancePlan> plans;
        public ObservableCollection<DcMaintenancePlan> Plans
        {
            get { return plans; }
            set { plans = value; RaisePropertyChanged(nameof(Plans)); }
        }

        #endregion

        #region Commands
        public RelayCommand Add { get; private set; }
        public RelayCommand<DcMaintenancePlan> Edit { get; private set; }
        public RelayCommand<DcMaintenancePlan> Duplicate { get; private set; }
        #endregion
    }
}

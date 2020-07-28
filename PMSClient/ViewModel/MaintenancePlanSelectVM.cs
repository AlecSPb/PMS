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
    public class MaintenancePlanSelectVM:BaseViewModelSelect
    {
        public MaintenancePlanSelectVM()
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

            Select = new RelayCommand<DcMaintenancePlan>(ActionSelect, CanSelect);
            GiveUp = new RelayCommand(ActionGive);

        }

        private void ActionGive()
        {
            NavigationService.GoTo(requestView);
        }

        private PMSViews requestView;
        /// <summary>
        /// 设置请求视图的token，返回或者选择后返回用
        /// </summary>
        /// <param name="request">请求视图的token</param>
        public void SetRequestView(PMSViews request)
        {
            requestView = request;
        }


        private void ActionSelect(DcMaintenancePlan plan)
        {
            if (plan != null)
            {
                switch (requestView)
                {
                    case PMSViews.MaintainRecordEdit:
                        PMSHelper.ViewModels.MaintenanceRecordEdit.SetBySelect(plan);
                        break;
                    default:
                        break;
                }
                NavigationService.GoTo(requestView);
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

        private bool CanSelect(DcMaintenancePlan arg)
        {
            return PMSHelper.CurrentSession.IsInGroup("MaintainPlan");
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchVHPDeviceCode));
        }

        private void ActionAll()
        {
            SearchVHPDeviceCode=SearchPlanItem = "";
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
        public RelayCommand<DcMaintenancePlan> Select { get; private set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class RecordVHPVM : BaseViewModelPage
    {
        public RecordVHPVM()
        {
            InitializeCommands();
            IntializeProperties();
            SetPageParametersWhenConditionChange();
            LoadRecordVHPsByRecordVHPID(MissonWithPlans.FirstOrDefault());
        }

        private void IntializeProperties()
        {
            SearchVHPID = "";
            MissonWithPlans = new ObservableCollection<DcMissonWithPlan>();
            RecordVHPs = new ObservableCollection<DcRecordVHP>();


            All = new RelayCommand(ActionAll);
            QuickEdit = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordVHPQuickEdit));
            SelectionChanged = new RelayCommand<DcMissonWithPlan>(ActionSelectionChanged);
        }

        private void ActionSelectionChanged(DcMissonWithPlan obj)
        {
            if (obj != null)
            {
                LoadRecordVHPsByRecordVHPID(obj);
            }
        }

        public void LoadRecordVHPsByRecordVHPID(DcMissonWithPlan model)
        {
            var result = (new RecordVHPServiceClient()).GetRecordVHP(model.PlanID);
            RecordVHPs.Clear();
            result.ToList().ForEach(i => RecordVHPs.Add(i));
        }

        private void ActionAll()
        {
            SetPageParametersWhenConditionChange();
            LoadRecordVHPsByRecordVHPID(MissonWithPlans.FirstOrDefault());
        }

        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 6;
            var service = new MissonServiceClient();
            RecordCount = service.GetMissonWithPlanCheckedCount();
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            var service = new MissonServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var orders = service.GetMissonWithPlanChecked(skip, take);
            MissonWithPlans.Clear();
            orders.ToList().ForEach(o => MissonWithPlans.Add(o));
        }

        #region Properties
        private string searchVHPID;

        public string SearchVHPID
        {
            get { return searchVHPID; }
            set { searchVHPID = value; RaisePropertyChanged(nameof(SearchVHPID)); }
        }

        public ObservableCollection<DcMissonWithPlan> MissonWithPlans { get; set; }
        public ObservableCollection<DcRecordVHP> RecordVHPs { get; set; }
        #endregion

        #region Commands

        public RelayCommand<DcMissonWithPlan> SelectionChanged { get; set; }
        public RelayCommand<DcMissonWithPlan> Doc { get; set; }


        public RelayCommand QuickEdit { get; set; }
        #endregion
    }
}

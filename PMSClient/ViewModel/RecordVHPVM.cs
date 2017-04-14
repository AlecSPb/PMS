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
            IntializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        private void IntializeProperties()
        {
            SearchPlanDate1 = DateTime.Now.AddDays(-90).Date;
            SearchPlanDate2 = DateTime.Now.AddDays(30).Date;
            MissonWithPlans = new ObservableCollection<DcMissonWithPlan>();
            RecordVHPs = new ObservableCollection<DcRecordVHP>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            SelectionChanged = new RelayCommand<DcMissonWithPlan>(ActionSelectionChanged);

            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            QuickEdit = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordVHPQuickEdit), 
                () => PMSHelper.CurrentSession.IsAuthorized("编辑热压记录"));

            Doc = new RelayCommand<MainService.DcMissonWithPlan>(ActionDoc, CanDoc);
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange(); 
        }

        private bool CanDoc(DcMissonWithPlan arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑热压记录");
        }

        private void ActionDoc(DcMissonWithPlan model)
        {
            //TODO:生成VHP报告
            //throw new NotImplementedException();
        }

        private void ActionSelectionChanged(DcMissonWithPlan model)
        {
            if (model != null)
            {
                LoadRecordVHPsByRecordVHPID(model);
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

        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 6;
            using (var service = new MissonServiceClient())
            {
                RecordCount = service.GetMissonWithPlanCheckedCountByDateRange(SearchPlanDate1, SearchPlanDate2);
            }
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
            //只显示Checked过的计划
            using (var service = new MissonServiceClient())
            {
                var orders = service.GetMissonWithPlanCheckedByDateRange(skip, take, SearchPlanDate1, SearchPlanDate2);
                MissonWithPlans.Clear();
                orders.ToList().ForEach(o => MissonWithPlans.Add(o));
                LoadRecordVHPsByRecordVHPID(MissonWithPlans.FirstOrDefault());
            }
        }

        #region Properties


        public ObservableCollection<DcMissonWithPlan> MissonWithPlans { get; set; }
        public ObservableCollection<DcRecordVHP> RecordVHPs { get; set; }

        private DateTime searchPlanDate1;
        public DateTime SearchPlanDate1
        {
            get { return searchPlanDate1; }
            set
            {
                if (value < searchPlanDate2)
                {
                    searchPlanDate1 = value;
                    RaisePropertyChanged(nameof(SearchPlanDate1));
                }
            }
        }

        private DateTime searchPlanDate2;
        public DateTime SearchPlanDate2
        {
            get { return searchPlanDate2; }
            set
            {
                if (value > searchPlanDate1)
                {
                    searchPlanDate2 = value;
                    RaisePropertyChanged(nameof(SearchPlanDate2));
                }
            }
        }
        #endregion

        #region Commands

        public RelayCommand<DcMissonWithPlan> SelectionChanged { get; set; }
        public RelayCommand<DcMissonWithPlan> Doc { get; set; }
        public RelayCommand QuickEdit { get; set; }
        #endregion
    }
}

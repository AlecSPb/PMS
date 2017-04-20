using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using PMSClient.ReportsHelper;

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
            searchPlanDate1 = DateTime.Now.AddDays(-90).Date;
            searchPlanDate2 = DateTime.Now.AddDays(30).Date;
            PlanWithMissons = new ObservableCollection<DcPlanWithMisson>();
            RecordVHPs = new ObservableCollection<DcRecordVHP>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            SelectionChanged = new RelayCommand<DcPlanWithMisson>(ActionSelectionChanged);

            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            QuickEdit = new RelayCommand(() => NavigationService.GoTo(PMSViews.RecordVHPQuickEdit),
                () => PMSHelper.CurrentSession.IsAuthorized("编辑热压记录"));

            Doc = new RelayCommand<MainService.DcPlanWithMisson>(ActionDoc, CanDoc);
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private bool CanDoc(DcPlanWithMisson arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("浏览热压记录");
        }

        private void ActionDoc(DcPlanWithMisson model)
        {
            if (model != null)
            {
                try
                {
                    //TODO:4.0.4.6版本再开放
                    //ReportVHP report = new ReportVHP();
                    //report.SetModel(model);
                    //report.Output();
                    //PMSDialogService.ShowYes("报告生成成功", "请在桌面查看生成的热压记录报告");
                    //NavigationService.ShowStatusMessage("请在桌面查看生成的热压记录报告");
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
        }

        private void ActionSelectionChanged(DcPlanWithMisson model)
        {
            if (model != null)
            {
                LoadRecordVHPsByRecordVHPID(model);
            }
        }

        public void LoadRecordVHPsByRecordVHPID(DcPlanWithMisson model)
        {
            if (model != null)
            {
                var service = new RecordVHPServiceClient();
                var result = service.GetRecordVHP(model.Plan.ID);
                service.Close();
                RecordVHPs.Clear();
                result.ToList().ForEach(i => RecordVHPs.Add(i));
            }
            else
            {
                RecordVHPs.Clear();
            }
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
                RecordCount = service.GetPlanWithMissonCheckedCountByDateRange(SearchPlanDate1, SearchPlanDate2);
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
                var orders = service.GetPlanWithMissonCheckedByDateRange(skip, take, SearchPlanDate1, SearchPlanDate2);
                PlanWithMissons.Clear();
                orders.ToList().ForEach(o => PlanWithMissons.Add(o));
                LoadRecordVHPsByRecordVHPID(PlanWithMissons.FirstOrDefault());
            }
        }

        #region Properties


        public ObservableCollection<DcPlanWithMisson> PlanWithMissons { get; set; }
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

        public RelayCommand<DcPlanWithMisson> SelectionChanged { get; set; }
        public RelayCommand<DcPlanWithMisson> Doc { get; set; }
        public RelayCommand QuickEdit { get; set; }
        #endregion
    }
}

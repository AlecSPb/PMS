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

        /// <summary>
        /// 综合查询
        /// </summary>
        /// <param name="vhpnumber"></param>
        public void SetSearch(string vhpnumber)
        {
            SearchComposition = "";
            SearchVHPDate = vhpnumber;
            SetPageParametersWhenConditionChange();

        }

        private void IntializeProperties()
        {
            searchComposition = searchVHPDate = "";
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
                () => PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordVHP));

            Doc = new RelayCommand<MainService.DcPlanWithMisson>(ActionDoc, CanDoc);
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private bool CanDoc(DcPlanWithMisson arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.ReadPlan);
        }

        private void ActionDoc(DcPlanWithMisson model)
        {
            if (model != null)
            {
                if (PMSDialogService.ShowYesNo("请问", "确定要在桌面生成热压报表吗？"))
                {
                    try
                    {
                        WordVHP report = new WordVHP();
                        report.SetModel(model);
                        report.Output();
                        PMSDialogService.ShowYes("报告生成成功", "请在桌面查看生成的热压记录报告\r\n如果排版变形，请全选更改为宋体");
                        NavigationService.Status("请在桌面查看生成的热压记录报告,如果排版变形，请全选更改为宋体");
                    }
                    catch (Exception ex)
                    {
                        PMSHelper.CurrentLog.Error(ex);
                    }
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
            SearchComposition = SearchVHPDate = "";
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 6;
            using (var service = new MissonServiceClient())
            {
                RecordCount = service.GetPlanExtraCount(SearchVHPDate, SearchComposition);
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
                var orders = service.GetPlanExtra(skip, take, SearchVHPDate, SearchComposition);
                PlanWithMissons.Clear();
                orders.ToList().ForEach(o => PlanWithMissons.Add(o));
                LoadRecordVHPsByRecordVHPID(PlanWithMissons.FirstOrDefault());
            }
        }

        #region Properties


        public ObservableCollection<DcPlanWithMisson> PlanWithMissons { get; set; }
        public ObservableCollection<DcRecordVHP> RecordVHPs { get; set; }

        private string searchComposition;
        public string SearchComposition
        {
            get { return searchComposition; }
            set { searchComposition = value; RaisePropertyChanged(nameof(searchComposition)); }
        }
        private string searchVHPDate;
        public string SearchVHPDate
        {
            get { return searchVHPDate; }
            set { searchVHPDate = value; RaisePropertyChanged(nameof(searchVHPDate)); }
        }

        #endregion

        #region Commands

        public RelayCommand<DcPlanWithMisson> SelectionChanged { get; set; }
        public RelayCommand<DcPlanWithMisson> Doc { get; set; }
        public RelayCommand QuickEdit { get; set; }
        #endregion
    }
}

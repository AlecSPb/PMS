using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.NewService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using PMSClient.AnlysisService;

namespace PMSClient.ViewModel
{
    public class PlanTraceVM : BaseViewModelPage
    {
        public PlanTraceVM()
        {
            IntitializeProperties();
            IntitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        private void ActionRefresh(string obj)
        {
            SetPageParametersWhenConditionChange();
        }

        public void SetSearch(string vhpnumber)
        {
            SearchVHPDate = vhpnumber;
            SetPageParametersWhenConditionChange();
        }

        private void IntitializeProperties()
        {
            SearchComposition = SearchVHPDate = SearchPMINumber = "";


            PlanTraces = new ObservableCollection<DcPlanTrace>();
        }

        private void IntitializeCommands()
        {
            GoToMisson = new RelayCommand(() => NavigationService.GoTo(PMSViews.Misson), CanGoToMisson);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);

            Label = new RelayCommand<DcPlanTrace>(ActionLabel);
            SearchMisson = new RelayCommand<DcPlanTrace>(ActionSearchMisson);
            SelectionChanged = new RelayCommand<DcPlanTrace>(ActionSelectionChanged);
            Output = new RelayCommand<DcPlanTrace>(ActionOutput);
            RecordSheet = new RelayCommand(ActionRecordSheet);
            ShowDeMold = new RelayCommand<DcPlanTrace>(ActionDeMold);
            ShowMachine = new RelayCommand<DcPlanTrace>(ActionShowMachine);
            ShowTest = new RelayCommand<DcPlanTrace>(ActionShowTest);
            ShowBonding = new RelayCommand<DcPlanTrace>(ActionShowBonding);
            ShowDelivery = new RelayCommand<DcPlanTrace>(ActionShowDelivery);
            ShowFailure = new RelayCommand<DcPlanTrace>(ActionShowFailure);
            Anlysis = new RelayCommand(ActionAnlysis);
        }

        private void ActionAnlysis()
        {
            try
            {
                //年月选择对话框
                var dialog = new WPFControls.YearDateDailog(0);

                if (dialog.ShowDialog() == false)
                {
                    return;
                }

                int year_start = dialog.YearStart;
                int month_start = dialog.MonthStart;
                int year_end = dialog.YearEnd;
                int month_end = dialog.MonthEnd;

                DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);


                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"时间范围:{startTime.ToShortDateString()}到{endTime.ToShortDateString()}");
                sb.AppendLine($"天数:{(endTime - startTime).TotalDays}");

                sb.AppendLine("***总发货靶材数包括一片毛坯切多片靶材的情况");

                using (var db = new AnlysisServiceClient())
                {
                    var result = db.GetStatistic(year_start, month_start, year_end, month_end);
                    foreach (var item in result)
                    {
                        sb.Append(item.Key);
                        sb.Append("=");
                        sb.AppendLine(item.Value.ToString());
                    }
                }
                var win = new ToolWindow.PlainTextWindow();
                win.Width = 900;
                win.Height = 600;
                win.ContentText = sb.ToString();
                win.ShowDialog();
            }
            catch (Exception ex)
            {
                PMSDialogService.ShowWarning(ex.Message);
            }
        }

        private void ActionShowDelivery(DcPlanTrace obj)
        {
            if (obj != null)
            {
                ShowDetails(obj.RecordDelivery);
            }
        }

        private void ActionShowFailure(DcPlanTrace obj)
        {
            if (obj != null)
            {
                ShowDetails(obj.RecordFailure);
            }
        }

        private void ActionShowBonding(DcPlanTrace obj)
        {
            if (obj != null)
            {
                ShowDetails(obj.RecordBonding);
            }
        }

        private void ActionShowTest(DcPlanTrace obj)
        {
            if (obj != null)
            {
                ShowDetails(obj.RecordTest);
            }
        }

        private void ActionShowMachine(DcPlanTrace obj)
        {
            if (obj != null)
            {
                ShowDetails(obj.RecordMachine);
            }
        }

        private void ActionDeMold(DcPlanTrace obj)
        {
            if (obj != null)
            {
                ShowDetails(obj.RecordDeMold);
            }
        }

        private void ShowDetails(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                var dialog = new WPFControls.NormalizedDataViewer();
                dialog.SetMainStrings(s);
                dialog.ShowDialog();
            }
        }

        private void ActionRecordSheet()
        {

        }

        /// <summary>
        /// 导出计划数据
        /// </summary>
        /// <param name="model"></param>
        private void ActionOutput(DcPlanTrace model)
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定要导出计划追踪数据吗？"))
            {
                return;
            }
            try
            {

                //年月选择对话框
                var dialog = new WPFControls.YearDateDailog(0);

                if (dialog.ShowDialog() == false)
                {
                    return;
                }

                int year_start = dialog.YearStart;
                int month_start = dialog.MonthStart;
                int year_end = dialog.YearEnd;
                int month_end = dialog.MonthEnd;


                var excel = new ExcelOutputHelper.ExcelOutputPlanTrace();
                excel.Intialize("计划追踪数据", "Data", 30);
                excel.SetParameter(year_start, month_start, year_end, month_end);
                excel.Output();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                PMSDialogService.Show(ex.Message);
            }

        }

        private bool CanGoToMisson()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.ReadMisson);
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchComposition) && string.IsNullOrEmpty(SearchVHPDate) && string.IsNullOrEmpty(SearchPMINumber));
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionSelectionChanged(DcPlanTrace model)
        {
            if (model != null)
            {
                CurrentPlanWithMisson = model;
            }
        }

        private void ActionSearchMisson(DcPlanTrace model)
        {
            if (model != null)
            {

            }
        }

        private void ActionLabel(DcPlanTrace model)
        {
            if (model != null)
            {

            }
        }
        private void ActionAll()
        {
            SearchComposition = SearchVHPDate = SearchPMINumber = "";

            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new AnlysisServiceClient())
            {
                RecordCount = service.GetPlanTraceCount(SearchVHPDate, SearchComposition, SearchPMINumber);
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
            using (var service = new AnlysisServiceClient())
            {
                var models = service.GetPlanTrace(skip, take, SearchVHPDate, SearchComposition, SearchPMINumber);
                PlanTraces.Clear();
                models.ToList().ForEach(o => PlanTraces.Add(o));
            }
            CurrentPlanWithMisson = PlanTraces.FirstOrDefault();
        }

        private DcPlanTrace currentPlanWithMisson;

        public DcPlanTrace CurrentPlanWithMisson
        {
            get { return currentPlanWithMisson; }
            set { currentPlanWithMisson = value; RaisePropertyChanged(nameof(CurrentPlanWithMisson)); }
        }

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
        private string searchPMINumber;
        public string SearchPMINumber
        {
            get { return searchPMINumber; }
            set { searchPMINumber = value; RaisePropertyChanged(nameof(searchPMINumber)); }
        }
        #region Commands
        public RelayCommand GoToMisson { get; set; }
        public RelayCommand<DcPlanTrace> Label { get; set; }
        public RelayCommand<DcPlanTrace> SearchMisson { get; set; }
        public RelayCommand<DcPlanTrace> SelectionChanged { get; set; }
        public RelayCommand<DcPlanTrace> Output { get; set; }
        public RelayCommand RecordSheet { get; set; }
        public RelayCommand Anlysis { get; set; }
        #endregion

        #region Properties
        public ObservableCollection<DcPlanTrace> PlanTraces { get; set; }

        //显示追踪细节信息
        public RelayCommand<DcPlanTrace> ShowDeMold { get; set; }
        public RelayCommand<DcPlanTrace> ShowMachine { get; set; }
        public RelayCommand<DcPlanTrace> ShowTest { get; set; }
        public RelayCommand<DcPlanTrace> ShowBonding { get; set; }
        public RelayCommand<DcPlanTrace> ShowDelivery { get; set; }
        public RelayCommand<DcPlanTrace> ShowFailure { get; set; }

        #endregion

    }
}

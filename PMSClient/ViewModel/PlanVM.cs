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

namespace PMSClient.ViewModel
{
    public class PlanVM : BaseViewModelPage
    {
        public PlanVM()
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
            PlanWithMissons = new ObservableCollection<DcPlanExtra>();
        }

        private void IntitializeCommands()
        {
            GoToMisson = new RelayCommand(() => NavigationService.GoTo(PMSViews.Misson), CanGoToMisson);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            Label = new RelayCommand<DcPlanExtra>(ActionLabel);
            SearchMisson = new RelayCommand<DcPlanExtra>(ActionSearchMisson);
            SelectionChanged = new RelayCommand<DcPlanExtra>(ActionSelectionChanged);
            Output = new RelayCommand<DcPlanExtra>(ActionOutput);
            RecordSheet = new RelayCommand(ActionRecordSheet);
            BatchLabel = new RelayCommand(ActionBatchLabel);
            Compare = new RelayCommand<DcPlanExtra>(ActionCompare);
        }

        private void ActionCompare(DcPlanExtra obj)
        {
            SearchComposition = obj.Misson.CompositionStandard.Trim();
            SetPageParametersWhenConditionChange();
        }

        private void ActionBatchLabel()
        {
            try
            {
                var tool = new ToolWindow.DateSelector();
                if (tool.ShowDialog() == false)
                    return;
                DateTime selectedDate = tool.SelectedDate;
                string searchCode = selectedDate.ToString("yyMMdd");
                using (var service = new NewServiceClient())
                {
                    var pageData = service.GetPlanExtra(0, 100, searchCode, "", "");
                    var ordered = pageData.OrderBy(i => i.Plan.PlanLot).ThenBy(i => i.Plan.SearchCode);

                    StringBuilder lb = new StringBuilder();
                    foreach (var item in ordered)
                    {
                        lb.AppendLine(VMHelper.PlanVMHelper.CreateLabel(item));
                    }
                    var win = new ToolWindow.LabelCopyWindow();
                    win.LabelInformation = lb.ToString();
                    win.Show();
                }
            }
            catch (Exception ex)
            {
                PMSDialogService.ShowWarning(ex.Message);
            }

        }

        private void ActionRecordSheet()
        {
            //生成取模记录单
            if (!PMSDialogService.ShowYesNo("请问", "确定生成记录单吗？"))
            {
                return;
            }
            try
            {
                var word = new ReportsHelperNew.ReportRecordDeMold();
                word.Intialize("取模记录单", 50);
                word.Output();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        /// <summary>
        /// 导出计划数据
        /// </summary>
        /// <param name="model"></param>
        private void ActionOutput(DcPlanExtra model)
        {
            if (!PMSDialogService.ShowYesNo("询问", "数据导出时间会比较长，请在弹出完成对话框之前不要进行其他操作。\r\n确定明白请点确定开始"))
            {
                return;
            }

            PMSDialogService.UnImplementyet();

            PMSDialogService.Show("数据导出完成到桌面，请右键-打开方式-Excel打开文件");

        }

        private bool CanGoToMisson()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.ReadMisson);
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchComposition) && string.IsNullOrEmpty(SearchVHPDate)
                && string.IsNullOrEmpty(SearchPMINumber));
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionSelectionChanged(DcPlanExtra model)
        {
            if (model != null)
            {
                CurrentPlanWithMisson = model;
            }
        }

        private void ActionSearchMisson(DcPlanExtra model)
        {
            if (model != null)
            {
                PMSHelper.ViewModels.Misson.SetSearchCondition("", model.Misson.PMINumber);
                NavigationService.GoTo(PMSViews.Misson);
            }
        }

        private void ActionLabel(DcPlanExtra model)
        {
            if (model != null)
            {
                var lcw = new ToolWindow.LabelCopyWindow();
                lcw.LabelInformation = VMHelper.PlanVMHelper.CreateLabel(model);
                lcw.Show();
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
            using (var service = new NewServiceClient())
            {
                RecordCount = service.GetPlanExtraCount(SearchVHPDate, SearchComposition, SearchPMINumber);
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
            using (var service = new NewServiceClient())
            {
                var models = service.GetPlanExtra(skip, take, SearchVHPDate, SearchComposition, SearchPMINumber);
                PlanWithMissons.Clear();
                models.ToList().ForEach(o => PlanWithMissons.Add(o));
            }
            CurrentPlanWithMisson = PlanWithMissons.FirstOrDefault();
        }

        private DcPlanExtra currentPlanWithMisson;

        public DcPlanExtra CurrentPlanWithMisson
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
        public RelayCommand<DcPlanExtra> Label { get; set; }
        public RelayCommand<DcPlanExtra> SearchMisson { get; set; }
        public RelayCommand<DcPlanExtra> SelectionChanged { get; set; }
        public RelayCommand<DcPlanExtra> Output { get; set; }
        public RelayCommand<DcPlanExtra> Compare { get; set; }
        public RelayCommand RecordSheet { get; set; }
        public RelayCommand BatchLabel { get; set; }
        #endregion

        #region Properties
        public ObservableCollection<DcPlanExtra> PlanWithMissons { get; set; }
        #endregion

    }
}

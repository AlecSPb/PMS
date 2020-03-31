using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using PMSClient.MainService;



namespace PMSClient.ViewModel
{
    public class RecordMillingVM : BaseViewModelPage
    {
        public RecordMillingVM()
        {

            searchVHPPlanLot = searchComposition = "";
            AllPowderWeight = 0;
            RecordMillings = new ObservableCollection<DcRecordMilling>();
            SetPageParametersWhenConditionChange();
            InitializeCommands();
        }

        /// <summary>
        /// 综合查询
        /// </summary>
        /// <param name="vhpnumber"></param>
        public void SetSearch(string vhpnumber)
        {
            SearchVHPPlanLot = vhpnumber;
            searchComposition = "";
            SetPageParametersWhenConditionChange();
        }


        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcRecordMilling>(ActionEdit, CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            SelectionChanged = new RelayCommand<DcRecordMilling>(ActionSelectionChanged);
            Label = new RelayCommand<DcRecordMilling>(ActionLabel);
            Calculator = new RelayCommand(ActionCalculator);
            QuickAdd = new RelayCommand(ActionQuickAdd, CanQuickAdd);
            Output = new RelayCommand(ActionOutput);
            Trace = new RelayCommand<DcRecordMilling>(ActionTrace);
            Sieve = new RelayCommand(ActionSieve);
            Fail = new RelayCommand<DcRecordMilling>(ActionFail, CanEdit);
        }

        private void ActionFail(DcRecordMilling obj)
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定要添加此记录到报废记录中吗？"))
            {
                return;
            }
            if (obj != null)
            {
                try
                {
                    using (var service = new FailureService.FailureServiceClient())
                    {
                        int check_exist_count = service.GetFailuresCountByProductID(obj.VHPPlanLot);

                        if (check_exist_count == 0)
                        {
                            var model = VMHelper.FailureVMHelper.GetNewFailure();
                            model.ProductID = obj.VHPPlanLot;
                            model.Stage = "制粉";
                            model.Composition = obj.Composition;
                            model.Details = obj.PMINumber;
                            model.Remark= obj.Details;
                            model.Problem = "制粉不成功";
                            model.Process = "无";

                            service.AddFailure(model);
                            PMSDialogService.Show("添加成功");

                        }
                        else
                        {
                            PMSDialogService.ShowWarning("报废库已存在");
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private void ActionSieve()
        {
            NavigationService.GoTo(PMSViews.MillingTool);
        }

        private void ActionTrace(DcRecordMilling obj)
        {
            if (obj != null && !string.IsNullOrEmpty(obj.RecycleID) && !obj.RecycleID.Contains("无"))
            {
                SearchVHPPlanLot = obj.RecycleID.Substring(0, 8);
                SetPageParametersWhenConditionChange();
            }
            else
            {
                PMSDialogService.ShowWarning("没有填写有效的回收ID");
            }
        }

        private void ActionOutput()
        {
            if (!PMSDialogService.ShowYesNo("询问", "数据导出时间会比较长，" +
                "请在弹出完成对话框之前不要进行其他操作。\r\n确定明白请点确定开始"))
            {
                return;
            }

            CsvOutputHelper.CsvOutputRecordMilling csv = new CsvOutputHelper.CsvOutputRecordMilling();
            csv.Output();
            PMSDialogService.Show("OK");
        }

        private bool CanQuickAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordMilling);
        }

        private void ActionQuickAdd()
        {
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.RecordMilling);
            PMSHelper.ViewModels.PlanSelect.RefreshData();
            NavigationService.GoTo(PMSViews.PlanSelect);
        }

        private void ActionLabel(DcRecordMilling model)
        {
            if (model != null)
            {
                var sb = new StringBuilder();
                sb.AppendLine(model.Composition);
                sb.AppendLine(model.PMINumber);
                sb.AppendLine(model.VHPPlanLot);
                sb.AppendLine("重量:");

                var mainContent = sb.ToString();

                //var pageTitle = "热压毛坯标签打印输出";
                //var tips = @"点击打开模板按钮，粘贴不同内容到模板合适位置，热压编号是自动生成的，可能不正确，请再自行修改，然后打印标签";
                //var template = "毛坯标签";
                //var helpimage = "productionlabel.png";
                //PMSHelper.ToolViewModels.LabelOutPut.SetAllParameters(PMSViews.RecordMilling, pageTitle,
                //    tips, template, mainContent, helpimage);
                //NavigationService.GoTo(PMSViews.LabelOutPut);

                //2017-12-18
                PMSClient.ToolWindow.LabelCopyWindow lcw = new ToolWindow.LabelCopyWindow();
                lcw.LabelInformation = mainContent;
                lcw.Show();
            }
        }

        private void ActionCalculator()
        {
            PMSHelper.ToolViewModels.MaterialNeedCalcualtion.SetRequestView(PMSViews.RecordMillingEdit);
            NavigationService.GoTo(PMSViews.MaterialNeedCalcuationTool);
        }

        private void ActionSelectionChanged(DcRecordMilling model)
        {
            if (model != null)
            {
                CurrentRecordMilling = model;
            }
        }

        private bool CanEdit(DcRecordMilling arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordMilling);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordMilling);
        }

        private void ActionAll()
        {
            SearchVHPPlanLot = SearchComposition = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionEdit(DcRecordMilling model)
        {
            if (model != null)
            {
                PMSHelper.ViewModels.RecordMillingEdit.SetEdit(model);
                NavigationService.GoTo(PMSViews.RecordMillingEdit);
            }
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.RecordMillingEdit.SetNew();
            NavigationService.GoTo(PMSViews.RecordMillingEdit);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            var service = new RecordMillingServiceClient();
            RecordCount = service.GetRecordMillingCountByVHPPlanLot(SearchVHPPlanLot, SearchComposition);
            service.Close();
            ActionPaging();
        }
        private void ActionPaging()
        {

            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new RecordMillingServiceClient();
            var models = service.GetRecordMillingsByVHPPlanLot(skip, take, SearchVHPPlanLot, SearchComposition);

            AllPowderWeight = service.GetAllPowderWeight() / 1000;
            service.Close();
            RecordMillings.Clear();
            models.ToList().ForEach(o => RecordMillings.Add(o));

            CurrentRecordMilling = RecordMillings.FirstOrDefault();
        }

        private string searchVHPPlanLot;
        public string SearchVHPPlanLot
        {
            get { return searchVHPPlanLot; }
            set { searchVHPPlanLot = value; RaisePropertyChanged(nameof(SearchVHPPlanLot)); }
        }

        private string searchComposition;
        public string SearchComposition
        {
            get { return searchComposition; }
            set { searchComposition = value; RaisePropertyChanged(nameof(SearchComposition)); }
        }
        private double allPowderWeight;
        public double AllPowderWeight
        {
            get
            {
                return allPowderWeight;
            }
            set
            {
                allPowderWeight = value; RaisePropertyChanged(nameof(AllPowderWeight));
            }
        }


        #region DerivedPart
        private DcRecordMilling currentRecordMilling;

        public DcRecordMilling CurrentRecordMilling
        {
            get { return currentRecordMilling; }
            set { currentRecordMilling = value; RaisePropertyChanged(nameof(CurrentRecordMilling)); }
        }

        public ObservableCollection<DcRecordMilling> RecordMillings { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcRecordMilling> Edit { get; set; }

        public RelayCommand<DcRecordMilling> SelectionChanged { get; set; }
        public RelayCommand Calculator { get; set; }

        public RelayCommand<DcRecordMilling> Label { get; set; }
        public RelayCommand<DcRecordMilling> Trace { get; set; }
        public RelayCommand<DcRecordMilling> Fail { get; set; }

        public RelayCommand QuickAdd { get; set; }

        public RelayCommand Output { get; set; }
        public RelayCommand Sieve { get; set; }
        #endregion
    }
}

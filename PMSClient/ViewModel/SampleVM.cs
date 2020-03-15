using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.Sample;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public class SampleVM : BaseViewModelPage
    {
        public SampleVM()
        {
            searchComposition = searchProductID = searchSampleType = "";
            Samples = new ObservableCollection<DcSample>();

            InitializeCommands();

            SampleTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SampleType>(SampleTypes);
            SampleTypes.Add("");

            SetPageParametersWhenConditionChange();
        }
        public List<string> SampleTypes { get; set; }
        private void InitializeCommands()
        {
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcSample>(ActionEdit, CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            Duplicate = new RelayCommand<DcSample>(ActionDuplicate, CanDuplicate);

            Prepared = new RelayCommand<DcSample>(ActionPrepared, CanQuickEdit);
            Checked = new RelayCommand<DcSample>(ActionChecked, CanQuickEdit);
            Sent = new RelayCommand<DcSample>(ActionSent, CanQuickEdit);
            Print = new RelayCommand(ActionPrint, CanPrint);
            Label = new RelayCommand<DcSample>(ActionLabel);
        }

        public bool CanQuickEdit(DcSample obj)
        {
            return PMSHelper.CurrentSession.IsOKInGroup(AccessGrant.Sample);
        }

        private void ActionLabel(DcSample model)
        {
            if (model != null)
            {
                if (string.IsNullOrEmpty(model.ProductID) || model.ProductID.Contains("无"))
                {
                    PMSDialogService.Show("请先准备样品并输入样品ID，再打印标签");
                    return;
                }

                //显示提示框给标签打印者
                StringBuilder lb = new StringBuilder();

                lb.AppendLine("=====  一般标签↑，样品标签↓  =====");
                lb.AppendLine();
                lb.AppendLine(model.Composition);
                lb.AppendLine("Weight      g");
                lb.AppendLine(model.ProductID);

                lb.AppendLine("=====  简成分样品标签↓  =====");
                lb.AppendLine(Helpers.CompositionHelper.RemoveNumbers(model.Composition));
                lb.AppendLine("Weight      g");
                lb.AppendLine(model.ProductID);
                lb.AppendLine();
                PMSHelper.ToolViewModels.LabelOutPut.SetAllParameters(PMSViews.Plan, "批量",
                        null, null, lb.ToString(), null);
                var win = new Tool.LabelOutPutWindow();
                win.Show();
            }
        }

        private bool CanPrint()
        {
            return true;
        }

        private void ActionPrint()
        {
            //弹出打印对话框
            var dialog = new ToolWindow.SingleCombBoxDialog();
            dialog.TxtCaption.Text = "请选择要打印类型，空为全部打印";
            List<string> ds = new List<string>();
            PMSMethods.SetListDS<PMSCommon.SampleType>(ds);
            ds.Add("");
            dialog.SetCboDatasource(ds);
            dialog.ShowDialog();

            if (dialog.DialogResult == true)
            {
                var printer = new ReportsHelperNew.ReportSample();
                printer.SelectedSampleType = dialog.CurrentSeletedString;
                string postfix = dialog.CurrentSeletedString == "" ? "全部" : dialog.CurrentSeletedString;
                printer.Intialize($"样品清单-{postfix}");
                printer.Output();
            }
        }

        private void ActionSent(DcSample obj)
        {
            AddProcess(obj, "已发出");
        }

        private void ActionChecked(DcSample obj)
        {
            AddProcess(obj, "已核验");
        }

        private void ActionPrepared(DcSample obj)
        {
            if (!PMSDialogService.ShowYesNo("请问", $"确定要准备该[{obj.Composition}-{obj.PMINumber}]样品吗？继续点是"))
            {
                return;
            }

            sample = obj;
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.Sample);
            PMSHelper.ViewModels.PlanSelect.SetSearchItem(composition: obj.Composition, searchlot: "", pminumber: "");
            PMSHelper.ViewModels.PlanSelect.RefreshData();

            NavigationService.GoTo(PMSViews.PlanSelect);
        }

        private DcSample sample;
        public void SetBySelect(DcPlanWithMisson plan)
        {

            if (plan != null)
            {
                sample.ProductID = plan.Plan.SearchCode + "-1";
                if (PMSDialogService.ShowYesNo("请问", $"确定要填入[{sample.ProductID}-{plan.Misson.CompositionStandard}]" +
                    $"到样品准备[{sample.Composition}-{sample.PMINumber}]吗？"))
                {
                    if (!(string.IsNullOrEmpty(sample.ProductID) || sample.ProductID.Contains("无")))
                    {
                        AddProcess(sample, "已准备");
                    }
                }
                //取消临时变量
                sample = null;
            }
        }
        private void AddProcess(DcSample obj, string type_name = "")
        {
            if (!PMSDialogService.ShowYesNo("请问", $"确定要为[{obj.ProductID}]追加[{type_name}]记录吗？"))
                return;

            if (obj == null) return;
            //分析要改变的类型
            PMSCommon.SampleType sampleType = PMSCommon.SampleType.未取样;
            switch (type_name)
            {
                case "已准备":
                    sampleType = PMSCommon.SampleType.未核验;
                    break;
                case "已核验":
                    sampleType = PMSCommon.SampleType.已核验;
                    break;
                case "已发出":
                    sampleType = PMSCommon.SampleType.已发出;
                    break;
                default:
                    break;
            }

            try
            {
                using (var s = new SampleServiceClient())
                {
                    obj.SampleType = sampleType.ToString();
                    obj.TraceInformation += $"{DateTime.Now.ToString("yyyy-MM-dd")}{type_name};";
                    s.UpdateSample(obj);
                }
                SetPageParametersWhenConditionChange();
            }
            catch (Exception ex)
            {
                PMSDialogService.ShowWarning(ex.Message);
            }
        }

        private void ActionDuplicate(DcSample obj)
        {
            if (PMSDialogService.ShowYesNo("请问", "确定复用这条记录？"))
            {
                if (obj != null)
                {
                    PMSHelper.ViewModels.SampleEdit.SetDuplicate(obj);
                    NavigationService.GoTo(PMSViews.SampleEdit);
                }
            }

        }

        private bool CanDuplicate(DcSample arg)
        {
            return PMSHelper.CurrentSession.IsOKInGroup(AccessGrant.SampleEdit);
        }

        private void ActionAll()
        {
            SearchProductID = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private bool CanEdit(DcSample arg)
        {
            return PMSHelper.CurrentSession.IsOKInGroup(AccessGrant.SampleEdit);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsOKInGroup(AccessGrant.SampleEdit);
        }

        private void ActionEdit(DcSample model)
        {
            PMSHelper.ViewModels.SampleEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.SampleEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.SampleEdit.SetNew();
            NavigationService.GoTo(PMSViews.SampleEdit);
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private string searchProductID;
        public string SearchProductID
        {
            get { return searchProductID; }
            set { searchProductID = value; RaisePropertyChanged(nameof(SearchProductID)); }
        }
        private string searchComposition;
        public string SearchComposition
        {
            get { return searchComposition; }
            set { searchComposition = value; RaisePropertyChanged(nameof(SearchComposition)); }
        }

        private string searchSampleType;
        public string SearchSampleType
        {
            get { return searchSampleType; }
            set { searchSampleType = value; RaisePropertyChanged(nameof(SearchSampleType)); }
        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new SampleServiceClient())
            {
                RecordCount = service.GetSampleAllCount(SearchProductID, SearchComposition, SearchSampleType);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new SampleServiceClient())
            {
                var orders = service.GetSampleAll(skip, take,
                    SearchProductID, SearchComposition, SearchSampleType);
                Samples.Clear();
                orders.ToList().ForEach(o => Samples.Add(o));
            }
        }
        #region Commands
        public ObservableCollection<DcSample> Samples { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcSample> Edit { get; set; }
        public RelayCommand<DcSample> Duplicate { get; set; }
        public RelayCommand<DcSample> Prepared { get; set; }
        public RelayCommand<DcSample> Checked { get; set; }
        public RelayCommand<DcSample> Sent { get; set; }
        public RelayCommand<DcSample> Label { get; set; }
        public RelayCommand Print { get; set; }

        #endregion

    }
}

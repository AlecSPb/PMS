using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
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
            searchComposition = searchVHPDate = "";
            PlanWithMissons = new ObservableCollection<DcPlanWithMisson>();
        }

        private void IntitializeCommands()
        {
            GoToMisson = new RelayCommand(() => NavigationService.GoTo(PMSViews.Misson), CanGoToMisson);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            GoToSearchPlan = new RelayCommand(() => NavigationService.GoTo(PMSViews.PlanSearch));
            Label = new RelayCommand<DcPlanWithMisson>(ActionLabel);
            SearchMisson = new RelayCommand<DcPlanWithMisson>(ActionSearchMisson);
            SelectionChanged = new RelayCommand<DcPlanWithMisson>(ActionSelectionChanged);
            Output = new RelayCommand<MainService.DcPlanWithMisson>(ActionOutput);
            RecordSheet = new RelayCommand(ActionRecordSheet);
        }

        private void ActionRecordSheet()
        {
            var tool = new ToolWindow.DateSelector();
            if (tool.ShowDialog() == false)
                return;
            DateTime selectedDate = tool.SelectedDate;
            PMSDialogService.ShowYes(selectedDate.ToShortDateString());
            //生成取模记录单
            //using (var service = new MissonServiceClient())
            //{
            //}



        }

        /// <summary>
        /// 导出计划数据
        /// </summary>
        /// <param name="model"></param>
        private void ActionOutput(DcPlanWithMisson model)
        {
            if (!PMSDialogService.ShowYesNo("询问", "数据导出时间会比较长，请在弹出完成对话框之前不要进行其他操作。\r\n确定明白请点确定开始"))
            {
                return;
            }

            int pageIndex = 1;
            int pageSize = 20;
            int recordCount = 0;
            using (var service = new MissonServiceClient())
            {
                recordCount = service.GetPlanExtraCount(SearchVHPDate, SearchComposition);
            }

            int pageCount = recordCount / PageSize + (recordCount % PageSize == 0 ? 0 : 1);

            int skip = 0, take = 0;
            take = pageSize;
            skip = (pageIndex - 1) * pageSize;

            string outputfile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
                , "导出数据-计划" + DateTime.Now.ToString("yyyyMMddmmhhss") + ".csv");
            StreamWriter sw = new StreamWriter(new FileStream(outputfile, FileMode.Append), System.Text.Encoding.GetEncoding("GB2312"));
            string titleString = "计划日期,批次,设备,计划类型,标准成分,内部编号,模具类型,内径,厚度,数量,密度,粉末粒径,单片,全部,湿度,室温,预压温度,预压压力,温度,压力,真空度,保温时间,工艺代码,制粉要求,装料要求,加工要求,转单,创建者,创建时间";
            sw.WriteLine(titleString);
            using (var service = new MissonServiceClient())
            {
                try
                {
                    string outputString = "";
                    while (pageIndex <= pageCount)
                    {
                        var models = service.GetPlanExtra(skip, take, SearchVHPDate, SearchComposition);

                        outputString = PMSOuputHelper.GetPlanOutput(models);

                        sw.Write(outputString.ToString());
                        sw.Flush();

                        pageIndex++;
                        skip = (pageIndex - 1) * pageSize;
                    }
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
            sw.Close();

            PMSDialogService.ShowYes("数据导出完成到桌面，请右键-打开方式-Excel打开文件");

        }

        private bool CanGoToMisson()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.ReadMisson);
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchComposition) && string.IsNullOrEmpty(SearchVHPDate));
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionSelectionChanged(DcPlanWithMisson model)
        {
            if (model != null)
            {
                CurrentPlanWithMisson = model;
            }
        }

        private void ActionSearchMisson(DcPlanWithMisson model)
        {
            if (model != null)
            {
                PMSHelper.ViewModels.Misson.SetSearchCondition("", model.Misson.PMINumber);
                NavigationService.GoTo(PMSViews.Misson);
            }
        }

        private void ActionLabel(DcPlanWithMisson model)
        {
            if (model != null)
            {
                if (model.Plan.PlanDate.Date >= DateTime.Now.Date)
                {
                    PMSDialogService.ShowWarning("选择的计划还没有热压，请确认你选择的【计划日期】是否正确？");
                }
                //显示提示框给标签打印者
                StringBuilder basic = new StringBuilder();
                basic.Append("共");
                basic.Append(model.Plan.Quantity);
                basic.Append("片");
                basic.AppendLine();
                basic.Append("制粉:");
                basic.Append(model.Plan.MillingRequirement);
                basic.AppendLine();
                basic.Append("装模:");
                basic.Append(model.Plan.FillingRequirement);
                basic.AppendLine();

                //如果是50.8mm的靶材且类型为加工，弹出打印多张的警告窗口
                if (model.Plan.PlanType.Contains("加工")
                    && model.Misson.Dimension.Contains("50.8"))
                {
                    basic.Append("50.8mm可能会切割多块，需要多个产品标签+样品标签");
                }

                var lb = new StringBuilder();
                lb.Append(UsefulPackage.PMSTranslate.PlanLot(model));
                lb.Append(" ");
                lb.Append(model.Plan.PlanType);
                lb.Append(" ");
                lb.AppendLine(model.Plan.ProcessCode);

                lb.AppendLine(model.Misson.CompositionStandard);
                lb.Append("模具:");
                lb.AppendLine(model.Plan.MoldDiameter.ToString() + "mm OD x " + model.Plan.Thickness + "mm");
                lb.Append("产品:");
                lb.AppendLine(model.Misson.Dimension);
                lb.Append("订单:");
                lb.AppendLine(model.Misson.PMINumber);
                lb.AppendLine();
                lb.AppendLine("=====  一般标签↑，样品标签↓  =====");
                lb.AppendLine();
                lb.AppendLine(model.Misson.CompositionStandard);
                lb.AppendLine("样品      g");
                lb.AppendLine(UsefulPackage.PMSTranslate.PlanLot(model));

                var lcw = new ToolWindow.LabelCopyWindow();
                lcw.LabelInformation = lb.ToString();
                lcw.BasicInformation = basic.ToString();
                lcw.Show();

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
            PageSize = 20;
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
            }
            CurrentPlanWithMisson = PlanWithMissons.FirstOrDefault();
        }

        private DcPlanWithMisson currentPlanWithMisson;

        public DcPlanWithMisson CurrentPlanWithMisson
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

        #region Commands
        public RelayCommand GoToMisson { get; set; }
        public RelayCommand GoToSearchPlan { get; set; }
        public RelayCommand<DcPlanWithMisson> Label { get; set; }
        public RelayCommand<DcPlanWithMisson> SearchMisson { get; set; }
        public RelayCommand<DcPlanWithMisson> SelectionChanged { get; set; }
        public RelayCommand<DcPlanWithMisson> Output { get; set; }
        public RelayCommand RecordSheet { get; set; }
        #endregion

        #region Properties
        public ObservableCollection<DcPlanWithMisson> PlanWithMissons { get; set; }
        #endregion

    }
}

using System;
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

        private void IntitializeProperties()
        {
            searchComposition = searchVHPDate = "";
            PlanWithMissons = new ObservableCollection<DcPlanWithMisson>();
        }

        private void IntitializeCommands()
        {
            GoToMisson = new RelayCommand(() => NavigationService.GoTo(PMSViews.Misson),CanGoToMisson);
            Search = new RelayCommand(ActionSearch,CanSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            GoToSearchPlan = new RelayCommand(() => NavigationService.GoTo(PMSViews.PlanSearch));
            Label = new RelayCommand<DcPlanWithMisson>(ActionLabel);
            SearchMisson = new RelayCommand<DcPlanWithMisson>(ActionSearchMisson);
            SelectionChanged = new RelayCommand<DcPlanWithMisson>(ActionSelectionChanged);
        }

        private bool CanGoToMisson()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.ReadMisson);
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchComposition)&&string.IsNullOrEmpty(SearchVHPDate));
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionSelectionChanged(DcPlanWithMisson model)
        {
            if (model!=null)
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

                var sb = new StringBuilder();
                sb.AppendLine(model.Misson.CompositionStandard);
                sb.AppendLine(model.Misson.Dimension);
                sb.AppendLine(model.Plan.PlanType);
                sb.AppendLine(UsefulPackage.PMSTranslate.PlanLot(model));

                var mainContent = sb.ToString();

                var pageTitle = "热压毛坯标签打印输出";
                var tips = @"点击打开模板按钮，粘贴不同内容到模板合适位置，热压编号是自动生成的，可能不正确，请再自行修改，然后打印标签";
                var template = "毛坯标签";
                var helpimage = "productionlabel.png";
                PMSHelper.ToolViewModels.LabelOutPut.SetAllParameters(PMSViews.Plan, pageTitle,
                    tips, template, mainContent, helpimage);
                NavigationService.GoTo(PMSViews.LabelOutPut);
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
        public RelayCommand<DcPlanWithMisson> SelectionChanged{ get; set; }
        #endregion

        #region Properties
        public ObservableCollection<DcPlanWithMisson> PlanWithMissons { get; set; }
        #endregion

    }
}
